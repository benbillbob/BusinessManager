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
	public class ArtistDetailViewModel : ViewModel, IDetailViewModel
	{
		IBusinessManagerEntities context;

		IBusinessManagerEntities Context
		{
			get
			{
				if (context == null)
				{
					context = Container.Current.Resolve<IBusinessManagerEntities>();
				}

				return context;
			}
		}

		Artist artist;

        public Artist Artist 
		{
			get
			{
				if (artist == null)
				{
					artist = new Artist();
				}

				return artist;
			}
		}

		public ICommand SaveCommand
		{
			get
			{
				return new RelayCommand(_ =>
				{
					if (Artist.Id == Guid.Empty)
					{
						Artist.Id = Guid.NewGuid();
						Context.AddArtist(Artist);
					}

					Context.Save();
				});
			}
		}

		public Guid Id
		{
			set
			{
				var db = Context.Artists;
				var q = from c in db
						where c.Id == value
						select c;

				artist = q.FirstOrDefault();
			}
		}
	}
}
