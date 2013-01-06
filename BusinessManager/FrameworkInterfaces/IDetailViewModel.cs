using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.FrameworkInterfaces
{
	public interface IDetailViewModel : IViewModel
	{
		Guid Id { set; }
	}
}
