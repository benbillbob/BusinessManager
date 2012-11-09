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

namespace BusinessManager.ViewModels
{
	public class StudentDetailViewModel : IDetailViewModel
	{
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
		
		Student student;
		
		public Student Student 
		{
			get
			{
				if (student == null)
				{
					student = new Student();
				}

				return student;
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
				}

				return choirs; 
			}
		}

		public ICommand StudentListCommand
		{
			get
			{
				return new RelayCommand(_ =>
				{
					var view = Container.Current.Resolve<IView>("StudentListView");
					var vm = Container.Current.Resolve<IViewModel>("StudentListViewModel");

					BusinessManager.FrameworkInterfaces.Navigation.Current.Show(view, vm);
				});
			}
		}

		public ICommand SaveCommand
		{
			get
			{
				return new RelayCommand(_ =>
				{
					if (Student.Id == Guid.Empty)
					{
						Student.Id = Guid.NewGuid();
						Context.AddStudent(Student);
					}

					Context.SaveChanges();
				});
			}
		}

		public Guid Id
		{
			set
			{
				var db = Context.Students;
				var q = from s in db
						where s.Id == value
						select s;

				student = q.FirstOrDefault();
			}
		}
	}
}
