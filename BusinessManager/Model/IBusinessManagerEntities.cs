using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BusinessManager.Model
{
	public interface IBusinessManagerEntities
	{
		IEnumerable<Student> Students { get; }
		IEnumerable<Choir> Choirs { get; }
		IEnumerable<Control> Controls { get; }
		IEnumerable<Roll> Rolls { get; }
		IEnumerable<StudentAttendence> StudentAttendences { get; }

		int Save();
		void AddStudent(Student student);
		void AddChoir(Choir choir);
		void AddControl(Control control);
		void AddStudentAttendence(StudentAttendence studentAttendence);
		void AddRoll(Roll roll);
	}

	public partial class BusinessManagerEntities : IBusinessManagerEntities
	{
		public void AddStudent(Student student)
		{
			Students.Add(student);
		}

		public void AddChoir(Choir choir)
		{
			Choirs.Add(choir);
		}

		public void AddControl(Control control)
		{
			Controls.Add(control);
		}

		public void AddRoll(Roll roll)
		{
			Rolls.Add(roll);
		}

		public void AddStudentAttendence(StudentAttendence studentAttendence)
		{
			StudentAttendences.Add(studentAttendence);
		}

		IEnumerable<Student> IBusinessManagerEntities.Students
		{
			get { return Students; }
		}

		IEnumerable<Roll> IBusinessManagerEntities.Rolls
		{
			get { return Rolls; }
		}

		IEnumerable<Choir> IBusinessManagerEntities.Choirs
		{
			get { return Choirs; }
		}

		IEnumerable<Control> IBusinessManagerEntities.Controls
		{
			get { return Controls; }
		}

		IEnumerable<StudentAttendence> IBusinessManagerEntities.StudentAttendences
		{
			get { return StudentAttendences; }
		}

		public virtual int Save()
		{
			return SaveChanges();
		}
	}
}