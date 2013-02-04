using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BusinessManager;
using BusinessManager.Framework;
using BusinessManager.FrameworkInterfaces;
using BusinessManager.Model;
using BusinessManager.Views;
using Microsoft.Practices.Unity;
using System.Windows.Data;
using System.IO;

namespace BusinessManager.ViewModels
{
	public class StudentListViewModel : ViewModel
	{
		CollectionViewSource students;

		public CollectionViewSource Students
		{
			get 
			{
				if (students == null)
				{
					students = new CollectionViewSource();
					students.Source = StudentsInternal;
					students.Filter += students_Filter;
					students.SortDescriptions.Add(new System.ComponentModel.SortDescription("LastName", System.ComponentModel.ListSortDirection.Ascending));
					PropertyChanged += StudentListViewModel_SelectedChoirIdPropertyChanged;
				}

				return students; 
			}
		}

		void StudentListViewModel_SelectedChoirIdPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "SelectedChoirId")
			{
				students.View.Refresh();
			}
		}

		void students_Filter(object sender, FilterEventArgs e)
		{
			var s = (Student)e.Item;
			e.Accepted = SelectedChoirId == Guid.Empty || SelectedChoirId == s.ChoirId;
		}
			
		List<Student> studentsInternal;

		List<Student> StudentsInternal
		{
			get
			{
				if (studentsInternal == null)
				{
					var context = Container.Current.Resolve<IBusinessManagerEntities>();
					var db = context.Students;

					studentsInternal = db.ToList();
				}

				return studentsInternal;
			}
		}

		public ICommand StudentSelectedCommand
		{
			get
			{
				return new RelayCommand(s =>
				{
					var view = Container.Current.Resolve<IView>("StudentDetailView");
					var vm = Container.Current.Resolve<IViewModel>("StudentDetailViewModel");

					((IDetailViewModel)vm).Id = ((Student)s).Id;

					Navigation.Show(view, vm);
				});
			}
		}

		public ICommand StudentAddCommand
		{
			get
			{
				return new RelayCommand(s =>
				{
					var view = Container.Current.Resolve<IView>("StudentDetailView");
					var vm = Container.Current.Resolve<IViewModel>("StudentDetailViewModel");

					Navigation.Show(view, vm);
				});
			}
		}

		public ICommand ImportStudents
		{
			get
			{
				return new RelayCommand(s =>
				{
					importStudents();
				});
			}
		}

		List<Choir> choirs;

		public List<Choir> Choirs
		{
			get
			{
				if (choirs == null)
				{
					var context = Container.Current.Resolve<IBusinessManagerEntities>();
					var q = from c in context.Choirs
							orderby c.Name
							select c;

					choirs = q.ToList();
					choirs.Insert(0, new Choir());
				}

				return choirs;
			}
		}

		Guid selectedChoirId;

		public Guid SelectedChoirId
		{
			get { return selectedChoirId; }
			set
			{
				if (selectedChoirId != value)
				{
					selectedChoirId = value;
					OnPropertyChanged("SelectedChoirId");
				}
			}
		}

		void importStudents()
		{
			var str = string.Empty;

			using (var file = new FileStream("students.txt", FileMode.Open))
			using (var stream = new StreamReader(file))
			{
				str = stream.ReadToEnd();
			}

			parseString(str);
		}

		void parseString(string str)
		{
			var fields = str.Split('|');

			var fieldNum = 56;
			var idx = 0;
			while (idx < (fields.Length / fieldNum))
			{
				var rec = fields.Skip(idx).Take(fieldNum);
				var student = createStudent(rec.ToArray());
				idx += fieldNum;
			}
		}

		Student createStudent(string[] rec)
		{
			var student = new Student();
			student.FirstName = rec[3];
			return student;
		}
	}
}
