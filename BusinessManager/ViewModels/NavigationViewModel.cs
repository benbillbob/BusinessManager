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
	public class NavigationViewModel : ViewModel
	{
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

		public ICommand StudentListCommand
		{
			get
			{
				return new RelayCommand(_ =>
				{
					var view = Container.Current.Resolve<IView>("StudentListView");
					var vm = Container.Current.Resolve<IViewModel>("StudentListViewModel");

					Navigation.Show(view, vm);
				});
			}
		}

		public ICommand ChoirListCommand
		{
			get
			{
				return new RelayCommand(_ =>
				{
					var view = Container.Current.Resolve<IView>("ChoirListView");
					var vm = Container.Current.Resolve<IViewModel>("ChoirListViewModel");

					Navigation.Show(view, vm);
				});
			}
		}
	}
}
