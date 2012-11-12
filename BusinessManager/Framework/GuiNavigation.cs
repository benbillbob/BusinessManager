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

		ScrollViewer navigationContainer;

		public ScrollViewer NavigationContainer
		{
			get
			{
				if (navigationContainer == null)
				{
					navigationContainer = (ScrollViewer)Viewer.FindName("Navigation");
				}
				return navigationContainer;
			}
			set { navigationContainer = value; }
		}

		ScrollViewer mainViewContainer;

		public ScrollViewer MainViewContainer
		{
			get
			{
				if (mainViewContainer == null)
				{
					mainViewContainer = (ScrollViewer)Viewer.FindName("MainView");
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
