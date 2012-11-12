using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessManager.Framework;
using BusinessManager.FrameworkInterfaces;
using NUnit.Framework;
using Microsoft.Practices.Unity;
using Moq;

namespace BusinessManagerTest.Framework
{
	[TestFixture]
	public class ViewModel_Test
	{
		class TestViewModel : ViewModel
		{
		}

		[Test]
		public void ViewModel_NavigationReturnsNavigation()
		{
			var mockNav = Mock.Of<INavigation>();
			Container.Current.RegisterInstance<INavigation>(mockNav);
			var t = new TestViewModel();
			Assert.That(t.Navigation, Is.Not.Null);
		}

		[Test]
		public void ViewModel_IsFullscreenReturnsFalse()
		{
			var t = new TestViewModel();
			Assert.That(t.IsFullScreen(), Is.False);
		}
	}
}
