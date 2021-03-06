﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using BusinessManager.Framework;
using BusinessManager.FrameworkInterfaces;
using BusinessManager.Model;
using BusinessManager.ViewModels;
using BusinessManager.Views;
using Microsoft.Practices.Unity;

namespace BusinessManager
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			MainWindow = new MainWindow();

			SetupContainer();

			DemoData.InitDemoData();

			ShowStartupWindow();
		}

		void ShowStartupWindow()
		{
			var view = Container.Current.Resolve<IView>("MainMenuView");
			var vm = Container.Current.Resolve<IViewModel>("MainMenuViewModel");

			MainWindow.Show();
			Navigation.Current.Show(view, vm);
		}

		public void SetupContainer()
		{
			Container.Current.RegisterInstance<INavigation>(new GuiNavigation());
			Container.Current.RegisterType(typeof(IView), typeof(StudentListView), "StudentListView");
			Container.Current.RegisterType(typeof(IViewModel), typeof(StudentListViewModel), "StudentListViewModel");
			Container.Current.RegisterType(typeof(IView), typeof(StudentDetailView), "StudentDetailView");
			Container.Current.RegisterType(typeof(IViewModel), typeof(StudentDetailViewModel), "StudentDetailViewModel");
			Container.Current.RegisterType(typeof(IView), typeof(ArtistListView), "ArtistListView");
			Container.Current.RegisterType(typeof(IViewModel), typeof(ArtistListViewModel), "ArtistListViewModel");
			Container.Current.RegisterType(typeof(IView), typeof(ArtistDetailView), "ArtistDetailView");
			Container.Current.RegisterType(typeof(IViewModel), typeof(ArtistDetailViewModel), "ArtistDetailViewModel");
			Container.Current.RegisterType(typeof(IView), typeof(RollListView), "RollListView");
			Container.Current.RegisterType(typeof(IViewModel), typeof(RollListViewModel), "RollListViewModel");
			Container.Current.RegisterType(typeof(IView), typeof(RollDetailView), "RollDetailView");
			Container.Current.RegisterType(typeof(IViewModel), typeof(RollDetailViewModel), "RollDetailViewModel");
			Container.Current.RegisterType(typeof(IView), typeof(ChoirListView), "ChoirListView");
			Container.Current.RegisterType(typeof(IViewModel), typeof(ChoirListViewModel), "ChoirListViewModel");
			Container.Current.RegisterType(typeof(IView), typeof(ChoirDetailView), "ChoirDetailView");
			Container.Current.RegisterType(typeof(IViewModel), typeof(ChoirDetailViewModel), "ChoirDetailViewModel");
			Container.Current.RegisterType(typeof(IView), typeof(MainMenuView), "MainMenuView");
			Container.Current.RegisterType(typeof(IViewModel), typeof(MainMenuViewModel), "MainMenuViewModel");
			Container.Current.RegisterType(typeof(IView), typeof(NavigationView), "NavigationView");
			Container.Current.RegisterType(typeof(IViewModel), typeof(NavigationViewModel), "NavigationViewModel");
			Container.Current.RegisterType(typeof(IBusinessManagerEntities), typeof(BusinessManagerEntities));
		}
	}
}
