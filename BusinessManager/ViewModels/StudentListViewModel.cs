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
using System.Data.Entity.Validation;

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
			var lines = str.Split(new string[] {"\r\n"}, StringSplitOptions.None);

			var context = Container.Current.Resolve<IBusinessManagerEntities>();

			for (var i = 0; i < lines.Length; i++)
			{
				var rec = lines[i].Split('|');
				if (rec.Length == 1)
				{
					continue;
				}

				var student = createStudent(rec);
				context.AddStudent(student);
			}

			try
			{
				context.Save();
			}
			catch (DbEntityValidationException ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		Student createStudent(string[] rec)
		{
			var student = new Student();

			student.Id = Guid.NewGuid();
			student.FirstName = rec[(int)Fields.FirstName];
			student.LastName = rec[(int)Fields.LastName];
			decimal tmp = 0;
			if (decimal.TryParse(rec[(int)Fields.Age], out tmp))
			{
				student.Age = tmp;
			}

			DateTime tmpDate;
			if (DateTime.TryParse(rec[(int)Fields.DOB], out tmpDate))
			{
				student.DOB = tmpDate;
			}

			student.Sex = rec[(int)Fields.Sex];
			student.School = rec[(int)Fields.School];
			student.ParentFirstName = rec[(int)Fields.ParentFirstName];
			student.ParentLastName = rec[(int)Fields.ParentLastName];
			student.Address = rec[(int)Fields.Addr1] + " " + rec[(int)Fields.Addr2] + " " + rec[(int)Fields.Addr3] + " " + rec[(int)Fields.Addr4];
			if (decimal.TryParse(rec[(int)Fields.PostCode], out tmp))
			{
				student.Postcode = tmp;
			} 
			student.HomePhone = rec[(int)Fields.HomePhone];
			student.WorkPhone = rec[(int)Fields.WorkPhone];
			student.MobilePhone = rec[(int)Fields.MobilePhone];
			student.Email = rec[(int)Fields.Email];
			student.Advertisement = rec[(int)Fields.Advertisement];
			//student.AuditionComments = rec[(int)Fields.AuditionComments];
			//student.Comments = rec[(int)Fields.Comments];
			//student.SpecialNeeds = rec[(int)Fields.SpecialNeeds];
			student.EmergencyContact1Name = rec[(int)Fields.EmergName1];
			student.EmergencyContact1HomePhone = rec[(int)Fields.EmergHomePhone1];
			student.EmergencyContact1WorkPhone = rec[(int)Fields.EmergWorkPhone1];
			student.EmergencyContact1MobilePhone = rec[(int)Fields.EmergMobilePhone1];
			student.EmergencyContact1RelationShip = rec[(int)Fields.EmergRel1];
			student.EmergencyContact2Name = rec[(int)Fields.EmergName2];
			student.EmergencyContact2HomePhone = rec[(int)Fields.EmergHomePhone2];
			student.EmergencyContact2WorkPhone = rec[(int)Fields.EmergWorkPhone2];
			student.EmergencyContact2MobilePhone = rec[(int)Fields.EmergMobilePhone2];
			student.EmergencyContact2Relationship = rec[(int)Fields.EmergRel2];
			student.ShirtSize = rec[(int)Fields.ShirtSize];

			return student;
		}

		enum Fields
		{
			PrimaryKey = 0,
			RecId,
			StudentId,
			FirstName,
			LastName,
			Age,
			DOB,
			Sex,
			School,
			ParentFirstName,
			ParentLastName,
			Addr1,
			Addr2,
			Addr3,
			Addr4,
			PostCode,
			HomePhone,
			WorkPhone,
			MobilePhone,
			Email,
			MusicalExp,
			Advertisement,
			AuditionComments,
			Level,
			LevelOld,
			FullPayment,
			FullPaymentOld,
			FirstPayment,
			FirstPaymentOld,
			SecondPayment,
			SecondPaymentOld,
			ThirdPayment,
			ThirdPaymentOld,
			FourthPayment,
			FourthPaymentOld,
			Comments,
			SpecialNeeds,
			EnrolmentReceived,
			EnrolmentReceivedOld,
			EmergName1,
			EmergHomePhone1,
			EmergWorkPhone1,
			EmergMobilePhone1,
			EmergRel1,
			EmergName2,
			EmergHomePhone2,
			EmergWorkPhone2,
			EmergMobilePhone2,
			EmergRel2,
			ShirtSize,
			ShirtSizeOld,
			ShirtReceived,
			ShirtReceivedOld,
			EmailContact,
			PostContact,
			TrialTime
		}
	}
}
