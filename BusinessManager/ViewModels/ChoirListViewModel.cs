using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BusinessManager;
using BusinessManager.Framework;
using BusinessManager.FrameworkInterfaces;
using BusinessManager.Model;
using BusinessManager.Views;
using Microsoft.Practices.Unity;

namespace BusinessManager.ViewModels
{
	public class ChoirListViewModel : ViewModel
	{
        List<Choir> choirs;

        public List<Choir> Choirs
		{
			get 
			{
                if (choirs == null)
				{
					var context = Container.Current.Resolve<BusinessManagerEntities>();
                    var db = context.Choirs;
					var q = from c in db
							select c;

                    choirs = q.ToList();
				}

                return choirs;
			}
		}

		public ICommand HomeCommand
		{
			get
			{
				return new RelayCommand(_ =>
				{
					var view = Container.Current.Resolve<IView>("MainMenuView");
					var vm = Container.Current.Resolve<IViewModel>("MainMenuViewModel");

					BusinessManager.FrameworkInterfaces.Navigation.Current.Show(view, vm);
				});
			}
		}

        public ICommand ChoirSelectedCommand
		{
			get
			{
				return new RelayCommand(c =>
				{
                    var view = Container.Current.Resolve<IView>("ChoirDetailView");
                    var vm = (DetailViewModel)Container.Current.Resolve<IViewModel>("ChoirDetailViewModel");

                    vm.Id = ((Choir)c).Id;

					Navigation.Show(view, vm);
				});
			}
		}

        public ICommand ChoirAddCommand
        {
            get
            {
                return new RelayCommand(c =>
                {
                    var view = Container.Current.Resolve<IView>("ChoirDetailView");
                    var vm = (DetailViewModel)Container.Current.Resolve<IViewModel>("ChoirDetailViewModel");

                    Navigation.Show(view, vm);
                });
            }
        }

    }
}
