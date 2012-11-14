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

namespace BusinessManagerTest.Model
{
	[TestFixture]
	[Ignore("Ignore for Now")]
	class BusinessManagerEntities_Test
	{
		[SetUp]
		public void Setup()
		{
			var mock = new Mock<IBusinessManagerEntities>() { CallBase = true };
			
			Container.Current.RegisterInstance<IBusinessManagerEntities>(mock.Object);

			var dummy = new DummyEntities();

			var context = Container.Current.Resolve<IBusinessManagerEntities>();

			foreach (var s in dummy.Students)
			{
				context.AddStudent(s);
			}

			foreach (var c in dummy.Choirs)
			{
				context.AddChoir(c);
			}
		}

		[Test]
		public void BusinessManagerEntities_UpdateRoll()
		{
			var context = Container.Current.Resolve<IBusinessManagerEntities>();

			var choir = context.Choirs.FirstOrDefault();
			var roll = new Roll();
			roll.ChoirId = choir.Id;

			context.AddRoll(roll);

			//context.UpdateRoll(roll);

			Assert.That(context.StudentAttendences.Count(), Is.EqualTo(1));
		}
	}
}
