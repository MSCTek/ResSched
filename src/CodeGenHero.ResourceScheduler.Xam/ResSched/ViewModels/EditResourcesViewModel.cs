using CodeGenHero.ResourceScheduler.Xam.ModelObj.RS;
using Microsoft.AppCenter.Crashes;
using ResSched.Mappers;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ResSched.ViewModels
{
    public class EditResourcesViewModel : BaseViewModel
    {
        private ObservableCollection<Resource> _resources;
        private bool _showNeedInternetMessage;

        public EditResourcesViewModel()
        {
        }

        public ObservableCollection<Resource> Resources
        {
            get { return _resources; }
            set { Set(nameof(Resources), ref _resources, value); }
        }

        public bool ShowNeedInternetMessage
        {
            get { return _showNeedInternetMessage; }
            set { Set(nameof(ShowNeedInternetMessage), ref _showNeedInternetMessage, value); }
        }

        public async Task InitVM()
        {
            try
            {
                if (base.Init())
                {
                    if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                    {
                        //refresh resources
                        await _dataLoadService.LoadResources();
                        Preferences.Set(Config.Preference_LastResourceUpdate, DateTime.UtcNow.ToString());
                        ShowNeedInternetMessage = false;
                    }
                    else
                    {
                        ShowNeedInternetMessage = true;
                    }
                    Resources = (await _dataService.GetAllResources()).ToObservableCollection();
                }
            }
            catch (Exception ex)
            {
                //this weird error - not sure why it fails
                Crashes.TrackError(ex);
                ShowNeedInternetMessage = true;
            }
        }
    }
}