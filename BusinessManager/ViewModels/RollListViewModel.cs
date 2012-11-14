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
using System.Windows.Data;

namespace BusinessManager.ViewModels
{
	public class RollListViewModel : ViewModel
	{
		CollectionViewSource rolls;

		public CollectionViewSource Rolls
		{
			get 
			{
				if (rolls == null)
				{
					rolls = new CollectionViewSource();
					rolls.Source = RollsInternal;
					rolls.Filter += rolls_Filter;
					PropertyChanged += RollListViewModel_SelectedChoirIdPropertyChanged;
				}

				return rolls; 
			}
		}

		void RollListViewModel_SelectedChoirIdPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "SelectedChoirId")
			{
				rolls.View.Refresh();
			}
		}

		void rolls_Filter(object sender, FilterEventArgs e)
		{
			var s = (Roll)e.Item;
			e.Accepted = SelectedChoirId == Guid.Empty || SelectedChoirId == s.ChoirId;
		}
			
		List<Roll> rollsInternal;

		List<Roll> RollsInternal
		{
			get
			{
				if (rollsInternal == null)
				{
					var context = Container.Current.Resolve<IBusinessManagerEntities>();
					var db = context.Rolls;

					rollsInternal = db.ToList();
				}

				return rollsInternal;
			}
		}

		public ICommand RollSelectedCommand
		{
			get
			{
				return new RelayCommand(s =>
				{
					var view = Container.Current.Resolve<IView>("RollDetailView");
					var vm = Container.Current.Resolve<IViewModel>("RollDetailViewModel");

					((IDetailViewModel)vm).Id = ((Roll)s).Id;

					Navigation.Show(view, vm);
				});
			}
		}

		public ICommand RollAddCommand
		{
			get
			{
				return new RelayCommand(s =>
				{
					var view = Container.Current.Resolve<IView>("RollDetailView");
					var vm = Container.Current.Resolve<IViewModel>("RollDetailViewModel");

					Navigation.Show(view, vm);
				});
			}
		}

		List<Choir> choirs;

		public List<Choir> Choirs
		{
			get
			{
				if (choirs == null)
				{
					var context = Container.Current.Resolve<IBusinessManagerEntities>();
					var q = from c in context.Choirs
							select c;

					choirs = q.ToList();
					choirs.Insert(0, new Choir());
				}

				return choirs;
			}
		}

		Guid selectedChoirId;

		public Guid SelectedChoirId
		{
			get { return selectedChoirId; }
			set
			{
				if (selectedChoirId != value)
				{
					selectedChoirId = value;
					OnPropertyChanged("SelectedChoirId");
				}
			}
		}
	}
}
