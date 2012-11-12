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

namespace BusinessManager.ViewModels
{
	public class StudentListViewModel : ViewModel
	{
		List<Student> students;
		ListCollectionView studentsView;

		public ListCollectionView Students
		{
			get
			{
				if (studentsView == null)
				{
					if (students == null)
					{
						var context = Container.Current.Resolve<BusinessManagerEntities>();
						var db = context.Students;
						var q = from s in db
								select s;

						students = q.ToList();
					}

					studentsView = new ListCollectionView(students);
					studentsView.Filter = new Predicate<object>((o) => 
					{
						if 
						(
							SelectedChoirId == null
							||
							SelectedChoirId != Guid.Empty
						)
						{
							return true;
						}

						if(((Student)o).ChoirId == SelectedChoirId)
						{
							return true;
						}
						else
						{
							return false;
						}
					});
				}

				return studentsView;
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

		List<Choir> choirs;

		public List<Choir> Choirs
		{
			get
			{
				if (choirs == null)
				{
					var context = Container.Current.Resolve<BusinessManagerEntities>();
					var q = from c in context.Choirs
							select c;

					choirs = q.ToList();
				}

				return choirs;
			}
		}

		Guid? selectedChoirId;

		public Guid? SelectedChoirId
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
	}
}
