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
		public ICommand HomeCommand { get { return navigationCommand("MainMenuView", "MainMenuViewModel"); } }
		public ICommand StudentListCommand { get { return navigationCommand("StudentListView", "StudentListViewModel"); } }
		public ICommand ChoirListCommand { get { return navigationCommand("ChoirListView", "ChoirListViewModel"); } }
		public ICommand RollListCommand { get { return navigationCommand("RollListView", "RollListViewModel"); } }
		public ICommand ArtistListCommand { get { return navigationCommand("ArtistListView", "ArtistListViewModel"); } }
		public ICommand SheetMusicListCommand { get { return navigationCommand("SheetMusicListView", "SheetMusicListViewModel"); } }

		RelayCommand navigationCommand(string view, string vm)
		{
			return new RelayCommand(_ =>
			{
				Navigation.Show(Container.Current.Resolve<IView>(view), Container.Current.Resolve<IViewModel>(vm));
			});
		}
	}
}
