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
	public class StudentDetailViewModel : DetailViewModel
	{
		BusinessManagerEntities context;

		BusinessManagerEntities Context
		{
			get
			{
				if (context == null)
				{
					context = Container.Current.Resolve<BusinessManagerEntities>();
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
					if (Id == Guid.Empty)
					{
						student = new Student();
					}
					else
					{
						var db = Context.Students;
						var q = from s in db
								where s.Id == Id
								select s;

						student = q.FirstOrDefault();
					}
				}

				return student;
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
						Context.Students.Add(Student);
					}

					Context.SaveChanges();
				});
			}
		}
	}
}
