
using System;
using System.Collections.Generic;
using System.Linq;
using BusinessManager.Model;

public class DummyEntities : IBusinessManagerEntities
{
	public DummyEntities()
	{
		choirAID = Guid.NewGuid();
		studentA = new Student() { FirstName = "StudentA", Id = Guid.NewGuid(), ChoirId = choirAID };
		choirA = new Choir() { Id = choirAID, Name = "ChoirA", Students = Students.ToList() };
		studentA.Choir = choirA;
	}

	Guid choirAID;
	Choir choirA;
	Student studentA;

	public IEnumerable<Student> Students { get { return new List<Student>() { studentA }; } }
	public IEnumerable<Choir> Choirs { get { return new List<Choir>() { choirA }; } }
	public IEnumerable<Control> Controls { get { return new List<Control>() { }; } }

	public virtual int Save()
	{
		return 0;
	}

	public virtual void AddStudent(Student student)
	{
		((List<Student>)Students).Add(student);
	}

	public virtual void AddChoir(Choir choir)
	{
		((List<Choir>)Choirs).Add(choir);
	}

	public virtual void AddControl(Control control)
	{
		((List<Control>)Controls).Add(control);
	}
}
