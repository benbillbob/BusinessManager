using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessManager.FrameworkInterfaces;

namespace BusinessManager.Framework
{
	public abstract class ViewModel : IViewModel
	{
		public INavigation Navigation
		{
			get { return BusinessManager.FrameworkInterfaces.Navigation.Current; }
		}
	}
}
