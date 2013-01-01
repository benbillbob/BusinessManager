
using System;
using System.Collections.Generic;
using System.Linq;
using BusinessManager.Model;

public class DummyEntities : IBusinessManagerEntities
{
	public DummyEntities()
	{
		choirAID = Guid.NewGuid();
		choirBID = Guid.NewGuid();
		studentA = new Student() { FirstName = "StudentA", Id = Guid.NewGuid(), ChoirId = choirAID };
		studentB = new Student() { FirstName = "StudentB", Id = Guid.NewGuid() };
		choirA = new Choir() { Id = choirAID, Name = "ChoirA", Students = (from s in Students where s.ChoirId == choirAID select s).ToList() };
		choirB = new Choir() { Id = choirBID, Name = "ChoirB", Students = (from s in Students where s.ChoirId == choirBID select s).ToList() };
		studentA.Choir = choirA;
		rollA = new Roll() { Id = Guid.NewGuid() };
		rollB = new Roll() { Id = Guid.NewGuid(), ChoirId = choirA.Id };
	}

	Guid choirAID;
	Choir choirA;
	Guid choirBID;
	Choir choirB;
	Student studentA;
	Student studentB;
	Roll rollA;
	Roll rollB;

	public IEnumerable<Student> Students { get { return new List<Student>() { studentA }; } }
	public IEnumerable<Choir> Choirs { get { return new List<Choir>() { choirA, choirB }; } }
	public IEnumerable<Control> Controls { get { return new List<Control>() { }; } }
	public IEnumerable<Roll> Rolls { get { return new List<Roll>() { rollA, rollB }; } }
	public IEnumerable<StudentAttendence> StudentAttendences { get { return new List<StudentAttendence>() { }; } }
	public IEnumerable<Artist> Artists { get { return new List<Artist>() { }; } }
	public IEnumerable<SheetMusic> SheetMusic { get { return new List<SheetMusic>() { }; } }

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

	public virtual void AddStudentAttendence(StudentAttendence studentAttendence)
	{
		((List<StudentAttendence>)StudentAttendences).Add(studentAttendence);
	}

	public virtual void AddRoll(Roll roll)
	{
		((List<Roll>)Rolls).Add(roll);
	}

	public void AddArtist(Artist artist)
	{
		((List<Artist>)Artists).Add(artist);
	}
}
