using ResSched.Interfaces;
using System.Threading.Tasks;

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
            var numResources = await _dataLoadService.LoadResources();
            var numResourceSchedules = await _dataLoadService.LoadResourceSchedules();
            DisplayMessage = $"Loaded: \n" +
                $"{numUsers} Users \n" +
                $"{numResources} Resources \n" +
                $"{numResourceSchedules} ResourceSchedules \n";

            //we just want to show them the welcome page briefly as we are loading data
            await Task.Delay(3000);

            Xamarin.Forms.Application.Current.MainPage = new Views.MainPage();
        }
    }
}