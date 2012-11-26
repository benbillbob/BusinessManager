using BusinessManager.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace BusinessManager.Model
{
    static class DemoData
    {
        public static void InitDemoData()
        {
            var context = Container.Current.Resolve<IBusinessManagerEntities>();
            var control = (from c in context.Controls
                           select c).FirstOrDefault();

			var students = new string[]{"Ben Woodcock", "Nat Woodcock", "Fred Durst", "Jimmy Dorantee", "Daniel Payne", "David Leader", "John Deeves"}.ToList();
			if (control == null)
            {
				var c = new Choir();
				c.Id = Guid.NewGuid();
				c.Name = "ChoirA";
				context.AddChoir(c);

				students.ForEach(n =>
				{
					var s = new Student();
					s.Id = Guid.NewGuid();
					var names = n.Split(' ');
					s.FirstName = names[0];
					s.LastName = names[1];
					s.ChoirId = c.Id;
					context.AddStudent(s);
				});

                c = new Choir();
                c.Id = Guid.NewGuid();
                c.Name = "ChoirB";
                context.AddChoir(c);

                c = new Choir();
                c.Id = Guid.NewGuid();
                c.Name = "ChoirC";
                context.AddChoir(c);

                control = new Control();
                control.Id = Guid.NewGuid();
                context.AddControl(control);

                context.Save();
            }
        }
    }
}
