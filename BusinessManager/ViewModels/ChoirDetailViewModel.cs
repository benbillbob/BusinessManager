﻿using System;
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
	public class ChoirDetailViewModel : DetailViewModel
	{
		BusinessManagerEntities context;

		BusinessManagerEntities Context
		{
			get
			{
				if (context == null)
				{
					context = Container.Current.Resolve<BusinessManagerEntities>();
				}

				return context;
			}
		}

        Choir choir;

        public Choir Choir 
		{
			get
			{
                if (choir == null)
				{
					if (Id == Guid.Empty)
					{
                        choir = new Choir();
					}
					else
					{
                        var db = Context.Choirs;
						var q = from c in db
								where c.Id == Id
								select c;

                        choir = q.FirstOrDefault();
					}
				}

                return choir;
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

					BusinessManager.FrameworkInterfaces.Navigation.Current.Show(view, vm);
				});
			}
		}

		public ICommand SaveCommand
		{
			get
			{
				return new RelayCommand(_ =>
				{
                    if (Choir.Id == Guid.Empty)
					{
                        Choir.Id = Guid.NewGuid();
                        Context.Choirs.Add(Choir);
					}

					Context.SaveChanges();
				});
			}
		}
	}
}