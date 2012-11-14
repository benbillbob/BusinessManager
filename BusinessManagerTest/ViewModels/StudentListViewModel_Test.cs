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
	public class StudentListViewModel_Test
	{
		StudentListViewModel vm;

		[SetUp]
		public void Setup()
		{
			Container.Current.RegisterInstance<IBusinessManagerEntities>(new DummyEntities());

			vm = new StudentListViewModel();
		}

		[Test]
		public void StudentListViewModel_Constructor()
		{
			Assert.That(vm, Is.Not.Null);
		}

		[Test]
		public void StudentListViewModel_Students()
		{
			var students = vm.Students;
			var studentA = Container.Current.Resolve<IBusinessManagerEntities>().Students.FirstOrDefault();
			Assert.That(students.View, Contains.Item(studentA));
		}

		[Test]
		public void StudentListViewModel_StudentAddCommand()
		{
			TestHelpers.NavigationTest("StudentDetailView", "StudentDetailViewModel", vm.StudentAddCommand);
		}

		[Test]
		public void StudentListViewModel_StudentSelectedCommand()
		{
			TestHelpers.NavigationTest("StudentDetailView", "StudentDetailViewModel", vm.StudentSelectedCommand, new Student());
		}

		[Test]
		public void StudentListViewModel_IsFullScreenReturnsFalse()
		{
			Assert.That(vm.IsFullScreen(), Is.False);
		}

		[Test]
		public void StudentListViewModel_AllStudentsMatchingFilters()
		{
			var data = Container.Current.Resolve<IBusinessManagerEntities>();

			var allStudents = data.Students.ToList();

			Assert.That(vm.Students.View, Is.EquivalentTo(allStudents));
	
			var choirId = data.Choirs.First().Id;

			var studentsInChoir = from s in data.Students
								  where s.ChoirId == choirId
								  select s;

			vm.SelectedChoirId = choirId;
			Assert.That(vm.Students.View, Is.EquivalentTo(studentsInChoir));
		}
		
		[Test]
		public void StudentListViewModel_Choirs()
		{
			var data = Container.Current.Resolve<IBusinessManagerEntities>();

			var allChoirs = data.Choirs.ToList();
			allChoirs.Insert(0, new Choir());

			Assert.That(vm.Choirs.Count(), Is.EqualTo(allChoirs.Count()));
		}
	}
}
