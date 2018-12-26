using ResSched.Mappers;
using ResSched.ObjModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ResSched.ViewModels
{
    public class MyReservationsViewModel : BaseViewModel
    {
        private ObservableCollection<ResourceSchedule> _reservations;

        public MyReservationsViewModel()
        {
            Title = "My Reservations";
        }

        public ObservableCollection<ResourceSchedule> Reservations
        {
            get { return _reservations; }
            set { Set(nameof(Reservations), ref _reservations, value); }
        }

        public async Task InitVM()
        {
            if (base.Init())
            {
                Reservations = (await base._dataService.GetResourceSchedulesForUser(App.AuthUserEmail)).ToObservableCollection();
            }
        }
    }
}