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
	public class SheetMusicDetailViewModel : ViewModel, IDetailViewModel
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

        SheetMusic sheetMusic;

        public SheetMusic SheetMusic 
		{
			get
			{
                if (sheetMusic == null)
				{
					sheetMusic = new SheetMusic();
				}

                return sheetMusic;
			}
		}

		public ICommand SaveCommand
		{
			get
			{
				return new RelayCommand(_ =>
				{
                    if (SheetMusic.Id == Guid.Empty)
					{
                        SheetMusic.Id = Guid.NewGuid();
                        Context.AddSheetMusic(SheetMusic);
					}

					Context.Save();
				});
			}
		}

		Artist selectedPerformer;
		public Artist SelectedPerformer
		{
			get
			{
				return selectedPerformer;
			}
			set
			{
				if (value != selectedPerformer)
				{
					selectedPerformer = value;
					OnPropertyChanged("SelectedPerformer");
				}
			}
		}

		public ICommand PerformerSelectedCommand
		{
			get 
			{
				return new RelayCommand(a => 
				{
					SelectedPerformer = (Artist)a;
				}); ; 
			}
		}

		Artist selectedComposer;
		public Artist SelectedComposer
		{
			get
			{
				return selectedComposer;
			}
			set
			{
				if (value != selectedComposer)
				{
					selectedComposer = value;
					OnPropertyChanged("SelectedComposer");
				}
			}
		}

		public ICommand ComposerSelectedCommand
		{
			get
			{
				return new RelayCommand(a =>
				{
					SelectedPerformer = (Artist)a;
				});
			}
		}

		public ICommand AddArtistCommand
		{
			get
			{
				return new RelayCommand(_ =>
				{
					var artistSelectedCommand = new RelayCommand(c =>
					{
						SheetMusic.Artists.Add((Artist)c);
						OnPropertyChanged("SheetMusic.Artists");
					});

					var view = Container.Current.Resolve<IView>("ArtistListView");
					var vm = Container.Current.Resolve<IViewModel>("ArtistListViewModel");

					((IListViewModel)vm).SelectedCommand = artistSelectedCommand;

					Navigation.ShowPopup(view, vm);
				});
			}
		}
		
		public ICommand RemoveArtistCommand
		{
			get
			{
				return new RelayCommand(_ =>
				{
				});
			}
		}

		public ICommand AddComposerCommand
		{
			get
			{
				return new RelayCommand(_ =>
				{
					var artistSelectedCommand = new RelayCommand(c =>
					{
						SheetMusic.Artists.Add((Artist)c);
						OnPropertyChanged("SheetMusic.Artists");
					});

					var view = Container.Current.Resolve<IView>("ArtistListView");
					var vm = Container.Current.Resolve<IViewModel>("ArtistListViewModel");

					((IListViewModel)vm).SelectedCommand = artistSelectedCommand;

					Navigation.ShowPopup(view, vm);
				});
			}
		}

		public ICommand RemoveComposerCommand
		{
			get
			{
				return new RelayCommand(_ =>
				{
				});
			}
		}

		public Guid Id
		{
			set
			{
                var db = Context.SheetMusics;
				var q = from c in db
						where c.Id == value
						select c;

                sheetMusic = q.FirstOrDefault();
			}
		}
	}
}
