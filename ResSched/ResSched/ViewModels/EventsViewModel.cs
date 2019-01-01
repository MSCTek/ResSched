using ResSched.Mappers;
using ResSched.Models.MeetupEvents;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ResSched.ViewModels
{
    public class EventsViewModel : BaseViewModel
    {
        private ObservableCollection<Result> _events;
        private Models.MeetupEvents.RootObject _root;

        public EventsViewModel()
        {
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

        public async Task InitVM()
        {
            if (base.Init())
            {
                Root = await Helpers.HTTPClientService.RefreshDataAsync();
                Events = Root.results.ToObservableCollection();
            }
        }
    }
}