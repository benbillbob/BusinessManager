using BusinessManager.Framework;
using BusinessManager.FrameworkInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Unity;
using Moq;
using NUnit.Framework;

namespace BusinessManagerTest.Framework
{
	public class DummyView : IView
	{
	}

	public class DummyViewModel : ViewModel, IViewModel, IDetailViewModel
	{
		public Guid Id { set { } }
	}

	public static class TestHelpers
	{
		public static void NavigationTest(string view, string viewModel, ICommand cmd, object arg = null)
		{
			Container.Current.RegisterType(typeof(IView), typeof(DummyView), view);
			Container.Current.RegisterType(typeof(IViewModel), typeof(DummyViewModel), viewModel);
			var mockNav = new Mock<INavigation>();
			Container.Current.RegisterInstance<INavigation>(mockNav.Object);

			Assert.That(cmd, Is.Not.Null);
			Assert.That(cmd, Is.TypeOf(typeof(RelayCommand)));

			cmd.Execute(arg);

			mockNav.Verify(x => x.Show(It.IsAny<IView>(), It.IsAny<IViewModel>()), Times.Exactly(1), "Failed to call show");
		}
	}
}
