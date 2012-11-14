using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessManager.Framework;
using BusinessManager.Model;
using Moq;
using NUnit.Framework;
using Microsoft.Practices.Unity;
using BusinessManager.ViewModels;

namespace BusinessManagerTest.ViewModels
{
	[TestFixture]
	class RollDetailViewModel_Test
	{
		RollDetailViewModel vm = null;

		[SetUp]
		public void Setup()
		{
			Container.Current.RegisterType(typeof(IBusinessManagerEntities), typeof(DummyEntities));
			vm = new RollDetailViewModel();
		}

		[Test]
		public void RollDetailViewModel_UpdateRoll()
		{
			var context = Container.Current.Resolve<IBusinessManagerEntities>();

			var choir = context.Choirs.FirstOrDefault();
			var roll = new Roll();
			roll.ChoirId = choir.Id;

			context.AddRoll(roll);

			vm.UpdateRoll();

			Assert.That(context.StudentAttendences.Count(), Is.EqualTo(1));
		}

	}
}
