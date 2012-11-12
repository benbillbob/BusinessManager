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
    public class ChoirDetailViewModel_Test
    {
        ChoirDetailViewModel vm;

        [SetUp]
        public void Setup()
        {
			Container.Current.RegisterInstance<IBusinessManagerEntities>(new DummyEntities());
			
			vm = new ChoirDetailViewModel();
        }

        [Test]
		public void ChoirDetailViewModel_Constructor()
        {
            Assert.That(vm, Is.Not.Null);
        }

		[Test]
		public void ChoirDetailViewModel_InitialisesNewChoirWithNoGuidAndNoStudents()
		{
			Assert.That(vm.Choir, Is.InstanceOf<Choir>());
			Assert.That(vm.Choir.Id, Is.EqualTo(Guid.Empty), "Id is Guid.Empty");
			Assert.That(vm.Choir.Name, Is.Null, "Name is Null");
			Assert.That(vm.Choir.Students, Is.Empty, "Students is Null");
		}

		[Test]
		public void ChoirDetailViewModel_InitialisesChoirWithChoirMatchingGuid()
		{
			var context = Container.Current.Resolve<IBusinessManagerEntities>();
			var testChoir = context.Choirs.FirstOrDefault();
			vm.Id = testChoir.Id;

			Assert.That(vm.Choir, Is.EqualTo(testChoir));
		}

		[Test]
		public void ChoirDetailViewModel_DoesNotCallAddChoirIfExisitingAndSaveChanges()
		{
			var context = Mock.Of<DummyEntities>();
			Container.Current.RegisterInstance<IBusinessManagerEntities>(context);
			var testChoir = context.Choirs.FirstOrDefault();

			vm.Id = testChoir.Id;
	
			vm.SaveCommand.Execute(null);

			var contextMock = Mock.Get(context);
			contextMock.Verify(x => x.AddChoir(It.Is<Choir>(s => s == vm.Choir)), Times.Exactly(0), "AddChoir called");
			contextMock.Verify(x => x.Save(), Times.Exactly(1), "Save not called");
		}

		[Test]
		public void ChoirDetailViewModel_CallsAddChoirIfNewAndSaveChanges()
		{
			var context = new Mock<IBusinessManagerEntities>();
			Container.Current.RegisterInstance<IBusinessManagerEntities>(context.Object);
			vm.SaveCommand.Execute(null);
			context.Verify(x => x.AddChoir(It.Is<Choir>(s => s == vm.Choir)), Times.Exactly(1));
			context.Verify(x => x.Save(), Times.Exactly(1));
		}

		[Test]
		public void ChoirDetailViewModel_IsFullScreenReturnsFalse()
		{
			Assert.That(vm.IsFullScreen(), Is.False);
		}
	}
}
