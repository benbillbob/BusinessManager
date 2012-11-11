using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessManager.Framework;
using BusinessManager.FrameworkInterfaces;
using NUnit.Framework;

namespace BusinessManagerTest.Framework
{
	[TestFixture]
	public class ViewModel_Test
	{
		class TestViewModel : ViewModel
		{
		}

		[Test]
		public void ViewModel_NavigationReturnsTypeOfNavigation()
		{
			var t = new TestViewModel();
			Assert.That(t.Navigation, Is.TypeOf(typeof(GuiNavigation)));
		}
	}
}
