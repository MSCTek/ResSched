using GalaSoft.MvvmLight.Command;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ResSched.ViewModels
{
    public class WelcomeViewModel : BaseViewModel
    {
        private string _displayMessage;
        private bool _showTryAgainButton;

        public WelcomeViewModel()
        {
            DisplayMessage = string.Empty;
            ShowTryAgainButton = false;
            IsBusy = false;
        }

        public string DisplayMessage
        {
            get { return _displayMessage; }
            set { Set(nameof(DisplayMessage), ref _displayMessage, value); }
        }

        public bool ShowTryAgainButton
        {
            get { return _showTryAgainButton; }
            set { Set(nameof(ShowTryAgainButton), ref _showTryAgainButton, value); }
        }

        public RelayCommand TryAgainCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    await Init();
                });
            }
        }

        public async Task Init()
        {
            DisplayMessage = string.Empty;
            ShowTryAgainButton = false;
            IsBusy = true;
            if (base.Init())
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    //if (await _dataLoadService.HeartbeatCheck())
                    if (true)
                    {
                        var numUsers = await _dataLoadService.LoadUsers();
                        DisplayMessage = $"Loading.";
                        Preferences.Set(Config.Preference_LastUserUpdate, DateTime.UtcNow.ToString());
                        await Task.Delay(500);

                        var numResources = await _dataLoadService.LoadResources();
                        DisplayMessage = $"Loading..";
                        Preferences.Set(Config.Preference_LastResourceUpdate, DateTime.UtcNow.ToString());
                        await Task.Delay(500);

                        var numResourceSchedules = await _dataLoadService.LoadResourceSchedules();
                        DisplayMessage = $"Loading...";
                        Preferences.Set(Config.Preference_LastResourceScheduleUpdate, DateTime.UtcNow.ToString());
                        await Task.Delay(500);

                        DisplayMessage = $"All Done";
                        IsBusy = false;
                        Xamarin.Forms.Application.Current.MainPage = new Views.MainPage();
                    }
                    else
                    {
                        //No API
                        DisplayMessage = $"Our services are down. Please try again later.";
                        IsBusy = false;
                        ShowTryAgainButton = true;
                    }
                }
                else
                {
                    //no connectivity
                    DisplayMessage = $"No internet connectivity. Please try again.";
                    IsBusy = false;
                    ShowTryAgainButton = true;
                }
            }
        }
    }
}