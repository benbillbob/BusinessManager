using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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

namespace BusinessManager.ViewModels
{
	public class RollDetailViewModel : ViewModel, IDetailViewModel
	{
		public RollDetailViewModel()
		{
		}

		void ChoirSelected(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "SelectedChoirId")
			{
				Roll.ChoirId = SelectedChoirId;
				UpdateRoll();
				//OnPropertyChanged("Students");
				//CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, Students));
			}
		}

		IBusinessManagerEntities context;

		IBusinessManagerEntities Context
		{
			get
			{
				if (context == null)
				{
					context = Container.Current.Resolve<IBusinessManagerEntities>();
				}

				return context;
			}
		}
		
		Roll roll;
		
		public Roll Roll 
		{
			get
			{
				if (roll == null)
				{
					roll = new Roll();
					PropertyChanged += ChoirSelected;
				}

				return roll;
			}
		}

		Collection<StudentAttendence> students;

		public Collection<StudentAttendence> Students
		{
			get
			{
				if (students == null)
				{
					var q = from s in Context.StudentAttendences
							where s.RollId == Roll.Id
							select s;

					students = new ObservableCollection<StudentAttendence>(q.ToList());
				}

				return students;
			}
		}

		List<Choir> choirs;

		public List<Choir> Choirs
		{
			get 
			{
				if (choirs == null)
				{
					choirs = Context.Choirs.ToList();
					choirs.Insert(0, new Choir());
				}

				return choirs; 
			}
		}

		public ICommand SaveCommand
		{
			get
			{
				return new RelayCommand(_ =>
				{
					if (Roll.Id == Guid.Empty)
					{
						Roll.Id = Guid.NewGuid();
						Context.AddRoll(Roll);

						foreach (var s in Students)
						{
							if (s.Id == Guid.Empty)
							{
								s.Id = Guid.NewGuid();
								Context.AddStudentAttendence(s);
							}

							s.RollId = Roll.Id;
						}
					}

					Context.Save();
					OnPropertyChanged("ChoirSelectionReadOnly");
				});
			}
		}

		public Guid Id
		{
			set
			{
				var db = Context.Rolls;
				var q = from s in db
						where s.Id == value
						select s;

				roll = q.FirstOrDefault();
				if (roll.ChoirId == null)
				{
					SelectedChoirId = Guid.NewGuid();
				}
				else
				{
					SelectedChoirId = (Guid)roll.ChoirId;
				}
			}
		}

		public bool ChoirSelectionReadOnly
		{
			get 
			{
				if (Roll.Id != Guid.Empty)
				{
					return true;
				}

				return false; 
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

		void AddStudentsToRoll()
		{
			var students = from student in Context.Students
						   where student.ChoirId == Roll.ChoirId
						   select student;

			var students2 = Context.Students.Where(x => x.ChoirId == Roll.ChoirId);

			var attendence = from sa in Students
							 where sa.RollId == Roll.Id
							 select sa;

			var q = from s in students
					where !((from a in attendence select a.StudentId).Contains(s.Id))
					select s;

			var q2 = students.Where(s => !attendence.Select(a => a.StudentId).Contains(s.Id));

			foreach (var ns in q)
			{
				var sa = new StudentAttendence();
				sa.StudentId = ns.Id;
				sa.Student = ns;
				sa.RollId = Roll.Id;
				Students.Add(sa);
			}
		}

		void RemoveStudentsFromRoll()
		{
			var q = (from s in Students
					where s.RollId == Roll.Id
					where s.Student.ChoirId != roll.ChoirId
					select s).ToArray();

			foreach (var s in q)
			{
				Students.Remove(s);
			}
		}

		public void UpdateRoll()
		{
			RemoveStudentsFromRoll();
			AddStudentsToRoll();
		}
	}
}
