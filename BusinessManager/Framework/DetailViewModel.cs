using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessManager.FrameworkInterfaces;

namespace BusinessManager.Framework
{
	public abstract class DetailViewModel : ViewModel, IViewModel
	{
		public Guid Id { get; set; }
	}
}
