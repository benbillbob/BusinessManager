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

            if (control == null)
            {
                var s = new Student();
                s.Id = Guid.NewGuid();
                s.FirstName = "Ben";
                context.AddStudent(s);

                var c = new Choir();
                c.Id = Guid.NewGuid();
                c.Name = "ChoirA";
                context.AddChoir(c);

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
