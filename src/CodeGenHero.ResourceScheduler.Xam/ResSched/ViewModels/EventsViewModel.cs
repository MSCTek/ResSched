using GalaSoft.MvvmLight.Command;
using Microsoft.AppCenter.Crashes;
using ResSched.Mappers;
using ResSched.Models.MeetupEvents;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ResSched.ViewModels
{
    public class EventsViewModel : BaseViewModel
    {
        private ObservableCollection<Result> _events;
        private bool _eventsSortAscending = true;
        private string _eventsSortDirectionText = string.Empty;
        private Models.MeetupEvents.RootObject _root;
        private bool _showNeedInternetMessage;

        public EventsViewModel()
        {
            Events = new ObservableCollection<Result>();
            Root = new RootObject();
        }

        public bool CanEventsSort
        {
            get { return Events != null && Events.Count > 0; }
        }

        public ObservableCollection<Result> Events
        {
            get { return _events; }
            set
            {
                Set(nameof(Events), ref _events, value);
                RaisePropertyChanged(nameof(CanEventsSort));
            }
        }

        public bool EventsSortAscending
        {
            get { return _eventsSortAscending; }
            set
            {
                if (value == true)
                {
                    Events = Events.SortByTime(ListSortDirection.Ascending);
                    EventsSortDirectionText = $"Events Feed from Meetup.com - Sorted {Enum.GetName(typeof(ListSortDirection), ListSortDirection.Ascending)}"; // {char.ConvertFromUtf32(0x2191)}";
                    ; //
                }
                else
                {
                    Events = Events.SortByTime(ListSortDirection.Descending);
                    EventsSortDirectionText = $"Events Feed from Meetup.com - Sorted {Enum.GetName(typeof(ListSortDirection), ListSortDirection.Descending)}"; // {char.ConvertFromUtf32(0x2193)}";
                }

                Set(nameof(EventsSortAscending), ref _eventsSortAscending, value);
            }
        }

        public RelayCommand EventsSortCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    try
                    {
                        EventsSortAscending = !EventsSortAscending;
                    }
                    catch (Exception ex)
                    {
                        ErrorDescription = ex.Message;
                        Crashes.TrackError(ex);
                    }
                });
            }
        }

        public string EventsSortDirectionText
        {
            get { return _eventsSortDirectionText; }
            set { Set(nameof(EventsSortDirectionText), ref _eventsSortDirectionText, value); }
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
                    EventsSortAscending = true; //.SortByTime(ListSortDirection.Ascending);
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