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
    public class StudentDetailViewModel_Test
    {
        StudentDetailViewModel vm;

        [SetUp]
        public void Setup()
        {
			Container.Current.RegisterInstance<IBusinessManagerEntities>(new DummyEntities());
			
			vm = new StudentDetailViewModel();
        }

        [Test]
		public void StudentDetailViewModel_Constructor()
        {
            Assert.That(vm, Is.Not.Null);
        }

		[Test]
		public void StudentDetailViewModel_Choirs()
		{
			var choirs = vm.Choirs;
			Assert.That(choirs.Count(), Is.EqualTo(1));
		}

		[Test]
		public void StudentDetailViewModel_InitialisesNewStudentWithNoGuid()
		{
			Assert.That(vm.Student, Is.InstanceOf<Student>());
			Assert.That(vm.Student.Id, Is.EqualTo(Guid.Empty), "Id is Guid.Empty");
			Assert.That(vm.Student.FirstName, Is.Null, "FirstName is Null");
			Assert.That(vm.Student.Choir, Is.Null, "Choir is Null");
			Assert.That(vm.Student.ChoirId, Is.Null, "ChoirId is Null");
		}

		[Test]
		public void StudentDetailViewModel_InitialisesStudentWithStudentMatchingGuid()
		{
			var context = Container.Current.Resolve<IBusinessManagerEntities>();
			var testStudent = context.Students.FirstOrDefault();
			vm.Id = testStudent.Id;

			Assert.That(vm.Student, Is.EqualTo(testStudent));
		}

		[Test]
		public void StudentDetailViewModel_DoesNotCallAddStudentIfExisitingAndSaveChanges()
		{
			var context = Mock.Of<DummyEntities>();
			Container.Current.RegisterInstance<IBusinessManagerEntities>(context);
			var testStudent = context.Students.FirstOrDefault();

			vm.Id = testStudent.Id;
	
			vm.SaveCommand.Execute(null);

			var contextMock = Mock.Get(context);
			contextMock.Verify(x => x.AddStudent(It.Is<Student>(s => s == vm.Student)), Times.Exactly(0), "AddStudent called");
			contextMock.Verify(x => x.Save(), Times.Exactly(1), "Save not called");
		}

		[Test]
		public void StudentDetailViewModel_CallsAddStudentIfNewAndSaveChanges()
		{
			var context = new Mock<IBusinessManagerEntities>();
			Container.Current.RegisterInstance<IBusinessManagerEntities>(context.Object);
			vm.SaveCommand.Execute(null);
			context.Verify(x => x.AddStudent(It.Is<Student>(s => s == vm.Student)), Times.Exactly(1));
			context.Verify(x => x.Save(), Times.Exactly(1));
		}

		[Test]
		public void StudentDetailViewModel_StudentListCommand()
		{
			TestHelpers.NavigationTest("StudentListView", "StudentListViewModel", vm.StudentListCommand);
		}
	}
}
