﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BusinessManager;
using BusinessManager.Framework;
using BusinessManager.FrameworkInterfaces;
using BusinessManager.Views;
using Microsoft.Practices.Unity;

namespace BusinessManager.ViewModels
{
	public class MainMenuViewModel : ViewModel
	{
		public ICommand StudentListCommand
		{
			get 
			{
				return new RelayCommand(_ => 
				{
					var view = Container.Current.Resolve<IView>("StudentListView");
					var vm = Container.Current.Resolve<IViewModel>("StudentListViewModel");

					Navigation.Show(view, vm); 
				});
			}
		}
    
        public ICommand ChoirListCommand
        {
            get
            {
                return new RelayCommand(_ =>
                {
                    var view = Container.Current.Resolve<IView>("ChoirListView");
                    var vm = Container.Current.Resolve<IViewModel>("ChoirListViewModel");

                    Navigation.Show(view, vm);
                });
            }
        }

		public ICommand RollListCommand
		{
			get
			{
				return new RelayCommand(_ =>
				{
					var view = Container.Current.Resolve<IView>("RollListView");
					var vm = Container.Current.Resolve<IViewModel>("RollListViewModel");

					Navigation.Show(view, vm);
				});
			}
		}

		public override bool IsFullScreen() { return true; } 
    }
}
