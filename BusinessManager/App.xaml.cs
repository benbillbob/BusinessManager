﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
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

			Container.Current.RegisterInstance<INavigation>(new GuiNavigation());
			Container.Current.RegisterType(typeof(IView), typeof(StudentListView), "StudentListView");
			Container.Current.RegisterType(typeof(IViewModel), typeof(StudentListViewModel), "StudentListViewModel");
			Container.Current.RegisterType(typeof(IView), typeof(StudentDetailView), "StudentDetailView");
			Container.Current.RegisterType(typeof(IViewModel), typeof(StudentDetailViewModel), "StudentDetailViewModel");
			Container.Current.RegisterType(typeof(IView), typeof(MainMenuView), "MainMenuView");
			Container.Current.RegisterType(typeof(IViewModel), typeof(MainMenuViewModel), "MainMenuViewModel");
			Container.Current.RegisterType(typeof(BusinessManagerEntities), typeof(BusinessManagerEntities));

			var context = Container.Current.Resolve<BusinessManagerEntities>();
			var s = new Student();
			s.Id = Guid.NewGuid();
			s.FirstName = "Ben";
			context.Students.Add(s);

			var c = new Choir();
			c.Id = Guid.NewGuid();
			c.Name = "ChoirA";
			context.Choirs.Add(c);

			context.SaveChanges();

			var view = Container.Current.Resolve<IView>("MainMenuView");
			var vm = Container.Current.Resolve<IViewModel>("MainMenuViewModel");

			MainWindow = new MainWindow();
			MainWindow.Show();
			Navigation.Current.Show(view, vm);
		}
	}
}
