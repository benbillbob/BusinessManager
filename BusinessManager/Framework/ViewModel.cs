using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessManager.FrameworkInterfaces;
using System.ComponentModel;

namespace BusinessManager.Framework
{
	public abstract class ViewModel : IViewModel, INotifyPropertyChanged
	{
		public INavigation Navigation
		{
			get { return BusinessManager.FrameworkInterfaces.Navigation.Current; }
		}

		public virtual bool IsFullScreen() { return false; }
		public event PropertyChangedEventHandler PropertyChanged;

		public void OnPropertyChanged(string property)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}
	}
}
