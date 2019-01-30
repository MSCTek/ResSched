using CodeGenHero.ResourceScheduler.Xam.ModelObj.RS;
using Microsoft.AppCenter.Crashes;
using ResSched.Mappers;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ResSched.ViewModels
{
    public class EditUsersViewModel : BaseViewModel
    {
        private bool _showNeedInternetMessage;
        private ObservableCollection<User> _users;

        public EditUsersViewModel()
        {
            Users = new ObservableCollection<User>();
        }

        public bool ShowNeedInternetMessage
        {
            get { return _showNeedInternetMessage; }
            set { Set(nameof(ShowNeedInternetMessage), ref _showNeedInternetMessage, value); }
        }

        public ObservableCollection<User> Users
        {
            get { return _users; }
            set { Set(nameof(Users), ref _users, value); }
        }

        public async Task InitVM()
        {
            try
            {
                if (base.Init())
                {
                    if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                    {
                        //refresh users
                        await _dataLoadService.LoadUsers();
                        Preferences.Set(Config.Preference_LastUserUpdate, DateTime.UtcNow.ToString());
                        ShowNeedInternetMessage = false;
                    }
                    else
                    {
                        ShowNeedInternetMessage = true;
                    }
                }
                Users = (await _dataService.GetAllUsers()).ToObservableCollection();
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