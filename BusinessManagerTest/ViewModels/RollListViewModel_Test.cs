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
using System.Windows.Data;
using System.Data.Entity.Infrastructure;
using Moq;
using BusinessManager.FrameworkInterfaces;
using BusinessManagerTest.Framework;

namespace BusinessManagerTest.ViewModels
{
	[TestFixture]
	public class RollListViewModel_Test
	{
		RollListViewModel vm;

		[SetUp]
		public void Setup()
		{
			Container.Current.RegisterInstance<IBusinessManagerEntities>(new DummyEntities());

			vm = new RollListViewModel();
		}

		[Test]
		public void RollListViewModel_Constructor()
		{
			Assert.That(vm, Is.Not.Null);
		}

		[Test]
		public void RollListViewModel_Rolls()
		{
			var rolls = vm.Rolls;
			var rollA = Container.Current.Resolve<IBusinessManagerEntities>().Rolls.FirstOrDefault();
			Assert.That(rolls.View, Contains.Item(rollA));
		}

		[Test]
		public void RollListViewModel_RollAddCommand()
		{
			TestHelpers.NavigationTest("RollDetailView", "RollDetailViewModel", vm.RollAddCommand);
		}

		[Test]
		public void RollListViewModel_RollSelectedCommand()
		{
			TestHelpers.NavigationTest("RollDetailView", "RollDetailViewModel", vm.RollSelectedCommand, new Roll());
		}

		[Test]
		public void RollListViewModel_IsFullScreenReturnsFalse()
		{
			Assert.That(vm.IsFullScreen(), Is.False);
		}

		[Test]
		public void RollListViewModel_AllRollsMatchingFilters()
		{
			var data = Container.Current.Resolve<IBusinessManagerEntities>();

			var allRolls = data.Rolls.ToList();

			Assert.That(vm.Rolls.View, Is.EquivalentTo(allRolls));
	
			var choirId = data.Choirs.First().Id;

			var RollsInChoir = from s in data.Rolls
								  where s.ChoirId == choirId
								  select s;

			vm.SelectedChoirId = choirId;
			Assert.That(vm.Rolls.View, Is.EquivalentTo(RollsInChoir));
		}
		
		[Test]
		public void RollListViewModel_Choirs()
		{
			var data = Container.Current.Resolve<IBusinessManagerEntities>();

			var allChoirs = data.Choirs.ToList();
			allChoirs.Insert(0, new Choir());

			Assert.That(vm.Choirs.Count(), Is.EqualTo(allChoirs.Count()));
		}
	}
}
