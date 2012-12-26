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
	public class ArtistListViewModel : ViewModel
	{
        List<Artist> artists;

        public List<Artist> Artists
		{
			get 
			{
                if (artists == null)
				{
					var context = Container.Current.Resolve<IBusinessManagerEntities>();
                    var db = context.Artists;
					var q = from c in db
							orderby c.Name
							select c;

                    artists = q.ToList();
				}

                return artists;
			}
		}

        public ICommand ArtistSelectedCommand
		{
			get
			{
				return new RelayCommand(c =>
				{
                    var view = Container.Current.Resolve<IView>("ArtistDetailView");
                    var vm = Container.Current.Resolve<IViewModel>("ArtistDetailViewModel");

                    ((IDetailViewModel)vm).Id = ((Artist)c).Id;

					Navigation.Show(view, vm);
				});
			}
		}

        public ICommand ArtistAddCommand
        {
            get
            {
                return new RelayCommand(c =>
                {
                    var view = Container.Current.Resolve<IView>("ArtistDetailView");
                    var vm = Container.Current.Resolve<IViewModel>("ArtistDetailViewModel");

                    Navigation.Show(view, vm);
                });
            }
        }

    }
}
