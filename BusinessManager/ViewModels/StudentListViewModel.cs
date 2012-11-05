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
	public class StudentListViewModel : ViewModel
	{
		List<Student> students;

		public List<Student> Students
		{
			get 
			{
				if (students == null)
				{
					var context = Container.Current.Resolve<BusinessManagerEntities>();
					var db = context.Students;
					var q = from s in db
							select s;

					students = q.ToList();
				}

				return students;
			}
		}

		public ICommand HomeCommand
		{
			get
			{
				return new RelayCommand(_ =>
				{
					var view = Container.Current.Resolve<IView>("MainMenuView");
					var vm = Container.Current.Resolve<IViewModel>("MainMenuViewModel");

					BusinessManager.FrameworkInterfaces.Navigation.Current.Show(view, vm);
				});
			}
		}

		public SelectionChangedEventHandler SelectionChanged
		{
			get
			{
				return new SelectionChangedEventHandler((s, e) => { MessageBox.Show(((Student)e.AddedItems[0]).FirstName); });
			}
		}

		public ICommand StudentSelectedCommand
		{
			get
			{
				return new RelayCommand(s =>
				{
					var view = Container.Current.Resolve<IView>("StudentDetailView");
					var vm = (DetailViewModel)Container.Current.Resolve<IViewModel>("StudentDetailViewModel");

					vm.Id = ((Student)s).Id;

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
					var vm = (DetailViewModel)Container.Current.Resolve<IViewModel>("StudentDetailViewModel");

					Navigation.Show(view, vm);
				});
			}
		}
	}
}
