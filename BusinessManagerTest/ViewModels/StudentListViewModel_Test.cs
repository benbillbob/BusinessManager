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
			Assert.That(students.Count(), Is.EqualTo(1));
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
	}
}
