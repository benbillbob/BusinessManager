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

namespace BusinessManagerTest.ViewModels
{
	[TestFixture]
	public class ChoirListViewModel_Test
	{
		ChoirListViewModel vm;

		[SetUp]
		public void Setup()
		{
			Container.Current.RegisterInstance<IBusinessManagerEntities>(new DummyEntities());

			vm = new ChoirListViewModel();
		}

		[Test]
		public void ChoirListViewModel_Constructor()
		{
			Assert.That(vm, Is.Not.Null);
		}

		[Test]
		public void ChoirListViewModel_Choirs()
		{
			var choirs = vm.Choirs;
			Assert.That(choirs.Count(), Is.EqualTo(1));
		}

		[Test]
		public void ChoirListViewModel_ChoirAddCommand()
		{
			TestHelpers.NavigationTest("ChoirDetailView", "ChoirDetailViewModel", vm.ChoirAddCommand);
		}

		[Test]
		public void ChoirListViewModel_ChoirSelectedCommand()
		{
			TestHelpers.NavigationTest("ChoirDetailView", "ChoirDetailViewModel", vm.ChoirSelectedCommand, new Choir());
		}

		[Test]
		public void ChoirListViewModel_IsFullScreenReturnsFalse()
		{
			Assert.That(vm.IsFullScreen(), Is.False);
		}
	}
}
