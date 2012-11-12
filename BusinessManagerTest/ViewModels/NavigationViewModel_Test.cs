using BusinessManager.Framework;
using BusinessManager.ViewModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BusinessManager.Model;
using Microsoft.Practices.Unity;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Moq;
using BusinessManager.FrameworkInterfaces;
using BusinessManagerTest.Framework;
using BusinessManager.Views;

namespace BusinessManagerTest.ViewModels
{
    [TestFixture]
    public class NavigationViewModel_Test
    {
		NavigationViewModel vm;

        [SetUp]
        public void Setup()
        {
			vm = new NavigationViewModel();
        }

        [Test]
		public void NavigationViewModel_Constructor()
        {
            Assert.That(vm, Is.Not.Null);
        }

		[Test]
		public void NavigationViewModel_NavigationTests()
		{
			var nav = new Mock<INavigation>();
			Container.Current.RegisterInstance<INavigation>(nav.Object);
			Container.Current.RegisterType(typeof(IView), typeof(DummyView), "StudentListView");
			Container.Current.RegisterType(typeof(IViewModel), typeof(DummyViewModel), "StudentListViewModel");
			Container.Current.RegisterType(typeof(IView), typeof(DummyView), "ChoirListView");
			Container.Current.RegisterType(typeof(IViewModel), typeof(DummyViewModel), "ChoirListViewModel");
			Container.Current.RegisterType(typeof(IView), typeof(DummyView), "MainMenuView");
			Container.Current.RegisterType(typeof(IViewModel), typeof(DummyViewModel), "MainMenuViewModel");
			vm.ChoirListCommand.Execute(null);
			nav.Verify(x => x.Show(It.Is<IView>(v => v.GetType() == typeof(DummyView)), It.Is<IViewModel>(newVm => newVm.GetType() == typeof(DummyViewModel))), Times.Exactly(1));

			vm.StudentListCommand.Execute(null);
			nav.Verify(x => x.Show(It.Is<IView>(v => v.GetType() == typeof(DummyView)), It.Is<IViewModel>(newVm => newVm.GetType() == typeof(DummyViewModel))), Times.Exactly(2));

			vm.HomeCommand.Execute(null);
			nav.Verify(x => x.Show(It.Is<IView>(v => v.GetType() == typeof(DummyView)), It.Is<IViewModel>(newVm => newVm.GetType() == typeof(DummyViewModel))), Times.Exactly(3));
		}

		[Test]
		public void StudentDetailViewModel_IsFullScreenReturnsFalse()
		{
			Assert.That(vm.IsFullScreen(), Is.False);
		}
	}
}
