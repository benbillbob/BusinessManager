using System.Collections.Generic;
using System.Data.Entity;
namespace BusinessManager.Model
{
	public interface IBusinessManagerEntities
	{
		IEnumerable<Student> Students { get; }
		IEnumerable<Choir> Choirs { get; }

		//DbSet<Student> Students { get; }
		//DbSet<Choir> Choirs { get; }
		int Save();
		void AddStudent(Student student);
	}

	public partial class BusinessManagerEntities : IBusinessManagerEntities
	{
		public void AddStudent(Student student)
		{
			Students.Add(student);
		}

		IEnumerable<Student> IBusinessManagerEntities.Students
		{
			get { return Students; }
		}

		IEnumerable<Choir> IBusinessManagerEntities.Choirs
		{
			get { return Choirs; }
		}

		public int Save()
		{
			return SaveChanges();
		}
	}
}