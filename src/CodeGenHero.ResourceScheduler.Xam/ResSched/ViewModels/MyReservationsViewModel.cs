using CodeGenHero.ResourceScheduler.Xam.ModelObj.RS;
using ResSched.Mappers;
using System;
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

        /*public RelayCommand<Guid> CancelReservationCommand
        {
            get
            {
                return new RelayCommand<Guid>(OnCancelReservation);
            }
        }*/

        public ObservableCollection<ResourceSchedule> Reservations
        {
            get { return _reservations; }
            set { Set(nameof(Reservations), ref _reservations, value); }
        }

        public async Task InitVM()
        {
            if (base.Init())
            {
                Reservations = (await base._dataService.GetResourceSchedulesForUser(App.AuthUserId)).ToObservableCollection();
            }
        }

        public async void OnCancelReservation(Guid resourceScheduleId)
        {
            await base._dataService.SoftDeleteReservation(resourceScheduleId);
            await InitVM();
        }
    }
}