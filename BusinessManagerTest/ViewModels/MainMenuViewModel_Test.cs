using BusinessManager.Framework;
using BusinessManager.ViewModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
        public void Constructor()
        {
            Assert.That(vm, Is.Not.Null);
        }

        [Test]
        public void StudentListCommand()
        {
            var cmd = vm.StudentListCommand;
            Assert.That(cmd, Is.Not.Null);
            Assert.That(cmd, Is.TypeOf(typeof(RelayCommand)));
        }
    }
}
