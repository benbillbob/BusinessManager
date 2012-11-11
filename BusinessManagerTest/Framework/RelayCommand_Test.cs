using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessManager.Framework;
using NUnit.Framework;
using Moq;

namespace BusinessManagerTest.Framework
{
	[TestFixture]
	public class RelayCommand_Test
	{
		[Test]
		public void RelayCommand_CanExecute()
		{
			var canExecute = true;
            var rc = new RelayCommand(_ => { }, (o) => { return canExecute; });

			Assert.That(rc.CanExecute(canExecute), Is.EqualTo(true));
			canExecute = false;
            Assert.That(rc.CanExecute(canExecute), Is.EqualTo(false));
		}

		//[Test]
		//public void RelayCommand_CanExecuteChanged()
		//{
		//	var notified = false;

		//	var rc = new RelayCommand(_ => { }, (o) => { return (bool)o; });
		//	rc.CanExecuteChanged += (s, e) => { notified = true; };
		//	rc.CanExecute(false);
		//	Assert.That(notified, Is.True);
		//}

		[Test]
		public void RelayCommand_ThrowsWhenNoActionPassed()
		{
			Assert.Throws(Is.TypeOf<ArgumentNullException>(), () => { new RelayCommand(null); });
		}
	}
}
