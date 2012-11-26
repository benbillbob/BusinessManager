using BusinessManager.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Unity;
using BusinessManager.FrameworkInterfaces;

namespace BusinessManager.Framework
{
	public class GuiNavigation : INavigation
	{
		public GuiNavigation()
		{
		}

		ContentControl navigationContainer;

		public ContentControl NavigationContainer
		{
			get
			{
				if (navigationContainer == null)
				{
					navigationContainer = (ContentControl)Viewer.FindName("Navigation");
				}
				return navigationContainer;
			}
			set { navigationContainer = value; }
		}

		ContentControl mainViewContainer;

		public ContentControl MainViewContainer
		{
			get
			{
				if (mainViewContainer == null)
				{
					mainViewContainer = (ContentControl)Viewer.FindName("MainView");
				}
				return mainViewContainer;
			}
			set { mainViewContainer = value; }
		}

		ContentControl viewer;

		ContentControl Viewer
		{
			get
			{
				if (viewer == null)
				{
					viewer = App.Current.MainWindow;
					var view = Container.Current.Resolve<IView>("NavigationView");
					var vm = Container.Current.Resolve<IViewModel>("NavigationViewModel");

					NavigationContainer.DataContext = vm;
					NavigationContainer.Content = view;
				}

				return viewer;
			}
		}

		public void Show(IView view, IViewModel vm)
		{
			((UserControl)view).DataContext = vm;
			MainViewContainer.Content = view;
			if (vm.IsFullScreen())
			{
				NavigationContainer.Visibility = Visibility.Hidden;
			}
			else
			{
				NavigationContainer.Visibility = Visibility.Visible;
			}
		}
	}
}
