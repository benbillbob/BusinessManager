using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessManager.Framework;
using NUnit.Framework;

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

			Assert.That(rc.CanExecute(this), Is.EqualTo(true));
			canExecute = true;
			Assert.That(rc.CanExecute(this), Is.EqualTo(false));
		}
	}
}
