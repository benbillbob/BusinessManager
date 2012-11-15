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
			var context = Mock.Of<DummyEntities>();
			var contextMock = Mock.Get(context);
			contextMock.CallBase = true;

			Container.Current.RegisterInstance(typeof(IBusinessManagerEntities), context);
			vm = new RollDetailViewModel();
		}

		[Test]
		public void RollDetailViewModel_UpdateRoll_AddStudents()
		{
			var context = Container.Current.Resolve<IBusinessManagerEntities>();
			var choirId = context.Choirs.FirstOrDefault().Id;
			var studentId = context.Students.FirstOrDefault().Id;

			Assert.That(vm.Students.Count(), Is.EqualTo(0));

			var roll = vm.Roll;
			vm.SelectedChoirId = choirId;

			Assert.That(vm.Students.Count(), Is.EqualTo(1));
		}

		[Test]
		public void RollDetailViewModel_UpdateRoll_NoStudents()
		{
			var context = Container.Current.Resolve<IBusinessManagerEntities>();
			var choirId = context.Choirs.Skip(1).First().Id;
			var studentId = context.Students.FirstOrDefault().Id;

			Assert.That(vm.Students.Count(), Is.EqualTo(0));

			var roll = vm.Roll;
			vm.SelectedChoirId = choirId;

			Assert.That(vm.Students.Count(), Is.EqualTo(0));
		}

		[Test]
		public void RollDetailViewModel_UpdateRoll_RemoveStudents()
		{
			var context = Container.Current.Resolve<IBusinessManagerEntities>();
			var choirId = context.Choirs.FirstOrDefault().Id;
			var studentId = context.Students.FirstOrDefault().Id;

			Assert.That(vm.Students.Count(), Is.EqualTo(0));

			var roll = vm.Roll;
			vm.SelectedChoirId = choirId;
			choirId = context.Choirs.Skip(1).First().Id;

			Assert.That(vm.Students.Count(), Is.EqualTo(1));
	
			vm.SelectedChoirId = choirId;

			Assert.That(vm.Students.Count(), Is.EqualTo(0));
		}

		[Test]
		public void RollDetailViewModel_Save()
		{
			var context = Container.Current.Resolve<IBusinessManagerEntities>();
			var contextMock = Mock.Get((DummyEntities)context);

			var choirId = context.Choirs.FirstOrDefault().Id;
			var studentId = context.Students.FirstOrDefault().Id;

			var roll = vm.Roll;
			vm.SelectedChoirId = choirId;

			vm.SaveCommand.Execute(null);

			contextMock.Verify(x => x.AddRoll(It.Is<Roll>(r => r == vm.Roll)), Times.Exactly(1));
			contextMock.Verify(x => x.AddStudentAttendence(It.Is<StudentAttendence>(r => r == vm.Students.First())), Times.Exactly(1));
		}
	}
}
