using ResSched.Mappers;
using ResSched.ObjModel;
using ResSched.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ResSched.ViewModels
{
    public class MyReservationsViewModel : BaseViewModel
    {
        private IDataRetrievalService _dataService;
        private ObservableCollection<ResourceSchedule> _reservations;

        public MyReservationsViewModel(IDataRetrievalService dataRetrievalService)
        {
            Title = "My Reservations";
            _dataService = dataRetrievalService;
        }

        public ObservableCollection<ResourceSchedule> Reservations
        {
            get { return _reservations; }
            set { Set(nameof(Reservations), ref _reservations, value); }
        }

        public async Task Init()
        {
            Reservations = (await _dataService.GetResourceSchedulesForUser(App.AuthUserEmail)).ToObservableCollection();
        }
    }
}