using CodeGenHero.ResourceScheduler.Xam.ModelObj.RS;
using ResSched.Mappers;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ResSched.ViewModels
{
    public class BrowseResourcesViewModel : BaseViewModel
    {
        private ObservableCollection<Resource> _items;

        public BrowseResourcesViewModel()
        {
            Title = "Browse Resources";
            Items = new ObservableCollection<Resource>();
        }

        public ObservableCollection<Resource> Items
        {
            get { return _items; }
            set { Set(nameof(Items), ref _items, value); }
        }

        public async Task InitVM()
        {
            if (base.Init())
            {
                //update the resources from the webAPI, if it has been more than an hour since it was done
                var lastResourceUpdate = Preferences.Get(Config.Preference_LastResourceUpdate, null);
                if (lastResourceUpdate != null)
                {
                    if ((DateTime.Parse(lastResourceUpdate)).AddHours(1) < DateTime.UtcNow)
                    {
                        await _dataLoadService.LoadResources();
                    }
                }

                Items = (await _dataService.GetAllResources()).ToObservableCollection();
            }
        }
    }
}