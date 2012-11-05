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
		Student student;
		
		public Student Student 
		{
			get
			{
				if (student == null)
				{
					if (Id == null)
					{
						student = new Student();
					}
					else
					{
						var context = Container.Current.Resolve<BusinessManagerEntities>();
						var db = context.Students;
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
					var context = Container.Current.Resolve<BusinessManagerEntities>();

					if (Student.Id == null)
					{
						Student.Id = Guid.NewGuid();
						context.Students.Add(Student);
					}
					else
					{
						context.Students.Attach(Student);
					}

					context.SaveChanges();
				});
			}
		}
	}
}
