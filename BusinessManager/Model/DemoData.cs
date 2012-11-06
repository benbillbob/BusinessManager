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
            var context = Container.Current.Resolve<BusinessManagerEntities>();
            var control = (from c in context.Controls
                           select c).FirstOrDefault();

            if (control == null)
            {
                var s = new Student();
                s.Id = Guid.NewGuid();
                s.FirstName = "Ben";
                context.Students.Add(s);

                var c = new Choir();
                c.Id = Guid.NewGuid();
                c.Name = "ChoirA";
                context.Choirs.Add(c);

                c = new Choir();
                c.Id = Guid.NewGuid();
                c.Name = "ChoirB";
                context.Choirs.Add(c);

                c = new Choir();
                c.Id = Guid.NewGuid();
                c.Name = "ChoirC";
                context.Choirs.Add(c);

                control = new Control();
                control.Id = Guid.NewGuid();
                context.Controls.Add(control);

                context.SaveChanges();
            }
        }
    }
}
