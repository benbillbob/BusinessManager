using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessManager.FrameworkInterfaces;

namespace BusinessManager.Framework
{
	public interface IDetailViewModel : IViewModel
	{
		Guid Id { set; }
	}
}
