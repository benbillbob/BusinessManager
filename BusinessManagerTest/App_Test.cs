using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using BusinessManager;
using BusinessManager.Framework;
using BusinessManager.FrameworkInterfaces;
using BusinessManager.Model;
using Microsoft.Practices.Unity;
using Moq;
using NUnit.Framework;

namespace BusinessManagerTest
{
	[TestFixture]
	public class App_Test
	{
		[Test]
		public void App_SetupContainer()
		{
			var app = new BusinessManager.App();
			app.SetupContainer();

			Assert.That(Container.Current.Resolve<INavigation>(), Is.Not.Null);
			Assert.That(Container.Current.Resolve<IViewModel>("StudentListViewModel"), Is.Not.Null);
			Assert.That(Container.Current.Resolve<IViewModel>("StudentDetailViewModel"), Is.Not.Null);
			Assert.That(Container.Current.Resolve<IViewModel>("ChoirListViewModel"), Is.Not.Null);
			Assert.That(Container.Current.Resolve<IViewModel>("ChoirDetailViewModel"), Is.Not.Null);
			Assert.That(Container.Current.Resolve<IViewModel>("MainMenuViewModel"), Is.Not.Null);
			Assert.That(Container.Current.Resolve<IBusinessManagerEntities>(), Is.Not.Null);
		}

		[Test]
		[RequiresSTA]
		public void TestMainWindow()
		{
			var win = new MainWindow();
			Assert.That(win, Is.Not.Null);

			var rc = new RelayCommand(_ => { });
			rc.CanExecuteChanged += rc_CanExecuteChanged;
			rc.CanExecuteChanged -= rc_CanExecuteChanged;
		}

		void rc_CanExecuteChanged(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}
