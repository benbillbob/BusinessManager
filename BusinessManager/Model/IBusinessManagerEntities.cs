using System.Collections.Generic;
using System.Data.Entity;
namespace BusinessManager.Model
{
	public interface IBusinessManagerEntities
	{
		IEnumerable<Student> Students { get; }
		IEnumerable<Choir> Choirs { get; }
		IEnumerable<Control> Controls { get; }

		int Save();
		void AddStudent(Student student);
		void AddChoir(Choir choir);
		void AddControl(Control control);
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

		public void AddControl(Control Control)
		{
			Controls.Add(Control);
		}

		IEnumerable<Student> IBusinessManagerEntities.Students
		{
			get { return Students; }
		}

		IEnumerable<Choir> IBusinessManagerEntities.Choirs
		{
			get { return Choirs; }
		}

		IEnumerable<Control> IBusinessManagerEntities.Controls
		{
			get { return Controls; }
		}

		public int Save()
		{
			return SaveChanges();
		}
	}
}