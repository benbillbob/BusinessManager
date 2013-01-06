using BusinessManager.Framework;
using BusinessManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using System.Windows.Input;
using BusinessManager.FrameworkInterfaces;

namespace BusinessManager.ViewModels
{
	class SheetMusicListViewModel : ViewModel
	{
		List<SheetMusic> sheetMusics;

		public List<SheetMusic> SheetMusics
		{
			get
			{
				if (sheetMusics == null)
				{
					var context = Container.Current.Resolve<IBusinessManagerEntities>();
					var db = context.SheetMusics;
					var q = from c in db
							orderby c.Title
							select c;

					sheetMusics = q.ToList();
				}

				return sheetMusics;
			}
		}

		public ICommand SheetMusicSelectedCommand
		{
			get
			{
				return new RelayCommand(c =>
				{
					var view = Container.Current.Resolve<IView>("SheetMusicDetailView");
					var vm = Container.Current.Resolve<IViewModel>("SheetMusicDetailViewModel");

					((IDetailViewModel)vm).Id = ((SheetMusic)c).Id;

					Navigation.Show(view, vm);
				});
			}
		}

		public ICommand SheetMusicAddCommand
		{
			get
			{
				return new RelayCommand(c =>
				{
					var view = Container.Current.Resolve<IView>("SheetMusicDetailView");
					var vm = Container.Current.Resolve<IViewModel>("SheetMusicDetailViewModel");

					Navigation.Show(view, vm);
				});
			}
		}
	}
}
