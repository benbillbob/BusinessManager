using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessManager.FrameworkInterfaces;
using BusinessManager.ViewModels;
using BusinessManager.Views;
using Microsoft.Practices.Unity;

namespace BusinessManager.Framework
{
	public static class Container
	{
		public static IUnityContainer Current
		{
			get
			{
				if (current == null)
				{
					current = new UnityContainer();
				}

				return current;
			}
		}
		static IUnityContainer current;
	}
}
