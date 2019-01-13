using ResSched.Interfaces;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ResSched.ViewModels
{
    public class WelcomeViewModel : BaseViewModel
    {
        private IDataLoadService _dataLoadService;
        private string _displayMessage;

        public WelcomeViewModel(IDataLoadService dataLoadService)
        {
            _dataLoadService = dataLoadService;
            DisplayMessage = string.Empty;
        }

        public string DisplayMessage
        {
            get { return _displayMessage; }
            set { Set(nameof(DisplayMessage), ref _displayMessage, value); }
        }

        public async Task Init()
        {
            DisplayMessage = string.Empty;

            var numUsers = await _dataLoadService.LoadUsers();
            DisplayMessage = $"Loading.";
            await Task.Delay(500);

            var numResources = await _dataLoadService.LoadResources();
            DisplayMessage = $"Loading..";
            Preferences.Set(Config.Preference_LastResourceUpdate, DateTime.UtcNow.ToString());
            await Task.Delay(500);

            var numResourceSchedules = await _dataLoadService.LoadResourceSchedules();
            DisplayMessage = $"Loading...";
            Preferences.Set(Config.Preference_LastResourceScheduleUpdate, DateTime.UtcNow.ToString());
            await Task.Delay(500);

            //we just want to show them the welcome page briefly as we are loading data
            await Task.Delay(500);

            DisplayMessage = $"All Done";
            Xamarin.Forms.Application.Current.MainPage = new Views.MainPage();
        }
    }
}