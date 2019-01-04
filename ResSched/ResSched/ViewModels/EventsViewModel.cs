using ResSched.Mappers;
using ResSched.Models.MeetupEvents;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ResSched.ViewModels
{
    public class EventsViewModel : BaseViewModel
    {
        private ObservableCollection<Result> _events;
        private Models.MeetupEvents.RootObject _root;
        private bool _showNeedInternetMessage;

        public EventsViewModel()
        {
            Events = new ObservableCollection<Result>();
            Root = new RootObject();
        }

        public ObservableCollection<Result> Events
        {
            get { return _events; }
            set { Set(nameof(Events), ref _events, value); }
        }

        public Models.MeetupEvents.RootObject Root
        {
            get { return _root; }
            set { Set(nameof(Root), ref _root, value); }
        }

        public bool ShowNeedInternetMessage
        {
            get { return _showNeedInternetMessage; }
            set { Set(nameof(ShowNeedInternetMessage), ref _showNeedInternetMessage, value); }
        }

        public async Task InitVM()
        {
            if (base.Init())
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    Root = await Helpers.HTTPClientService.RefreshDataAsync();
                    Events = Root.results.ToObservableCollection();
                    ShowNeedInternetMessage = false;
                }
                else
                {
                    ShowNeedInternetMessage = true;
                }
            }
        }
    }
}