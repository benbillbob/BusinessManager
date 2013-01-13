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
		public SheetMusicDetailViewModel()
		{
		}

		void Composers_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			ArtistCollectionChanged(SheetMusic.Composers, e);
		}

		void Artists_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			ArtistCollectionChanged(SheetMusic.Artists, e);
		}

		void ArtistCollectionChanged(ICollection<Artist> collection, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
			{
				foreach (var a in e.NewItems)
				{
					collection.Add((Artist)a);
				}
			}
			else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
			{
				foreach (var a in e.OldItems)
				{
					collection.Remove((Artist)a);
				}
			}
		}
		
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
					Artists.CollectionChanged += Artists_CollectionChanged;
					Composers.CollectionChanged += Composers_CollectionChanged;
				}

                return sheetMusic;
			}
			private set
			{
				sheetMusic = value;
				Artists.CollectionChanged += Artists_CollectionChanged;
				Composers.CollectionChanged += Composers_CollectionChanged;
			}
		}

		ObservableCollection<Artist> artists;

		public ObservableCollection<Artist> Artists
		{
			get 
			{
				if (artists == null)
				{
					artists = new ObservableCollection<Artist>(SheetMusic.Artists.ToArray());
				}

				return artists; 
			}
			set { artists = value; }
		}

		ObservableCollection<Artist> composers;

		public ObservableCollection<Artist> Composers
		{
			get
			{
				if (composers == null)
				{
					composers = new ObservableCollection<Artist>(SheetMusic.Composers.ToArray());
				}

				return composers;
			}
			set { composers = value; }
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
				}); 
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
					SelectedComposer = (Artist)a;
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
						if (!Artists.Contains((Artist)(c)))
						{
							Artists.Add((Artist)c);
						}
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
					if (SelectedPerformer != null)
					{
						Artists.Remove(SelectedPerformer);
					}
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
						if (!Composers.Contains((Artist)c))
						{
							Composers.Add((Artist)c);
						}
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
					if (SelectedComposer != null)
					{
						Composers.Remove(SelectedComposer);
					}
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

                SheetMusic = q.FirstOrDefault();
			}
		}
	}
}
