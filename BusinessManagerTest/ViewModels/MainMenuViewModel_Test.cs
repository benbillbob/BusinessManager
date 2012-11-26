using BusinessManager.Framework;
using BusinessManager.FrameworkInterfaces;
using BusinessManager.ViewModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Unity;
using Moq;
using BusinessManagerTest.Framework;

namespace BusinessManagerTest.ViewModels
{
    [TestFixture]
    public class MainMenuViewModel_Test
    {
        MainMenuViewModel vm;

        [SetUp]
        public void Setup()
        {
            vm = new MainMenuViewModel();
        }

        [Test]
		public void MainMenuViewModel_Constructor()
        {
            Assert.That(vm, Is.Not.Null);
        }

        [Test]
		public void MainMenuViewModel_StudentListCommand()
        {
			TestHelpers.NavigationTest("StudentListView", "StudentListViewModel", vm.StudentListCommand);
		}

        [Test]
        public void MainMenuViewModel_ChoirListCommand()
        {
			TestHelpers.NavigationTest("ChoirListView", "ChoirListViewModel", vm.ChoirListCommand);
		}

		[Test]
		public void MainMenuViewModel_RollListCommand()
		{
			TestHelpers.NavigationTest("RollListView", "RollListViewModel", vm.RollListCommand);
		}

		[Test]
		public void MainMenuViewModel_IsFullScreenReturnsTrue()
		{
			Assert.That(vm.IsFullScreen(), Is.True);
		}
    }
}
