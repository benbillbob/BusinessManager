using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BusinessManager.FrameworkInterfaces
{
	public class GuiNavigation : INavigation
	{
		public GuiNavigation()
		{
		}

		Window window;

		Window Window
		{
			get
			{
				if (window == null)
				{
					window = App.Current.MainWindow;
				}

				return window;
			}
		}

		public void Show(IView view, IViewModel vm)
		{
			((UserControl)view).DataContext = vm;
			Window.Content = view;
		}
	}
}
