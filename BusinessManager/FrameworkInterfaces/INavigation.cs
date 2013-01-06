using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using BusinessManager.Framework;
using BusinessManager.ViewModels;
using Microsoft.Practices.Unity;

namespace BusinessManager.FrameworkInterfaces
{
	public interface INavigation
	{
		void Show(IView view, IViewModel vm);
		void ShowPopup(IView view, IViewModel vm);
	}

	public static class Navigation
	{
		public static INavigation Current
		{
			get { return Container.Current.Resolve<INavigation>(); }
		}
	}
}
