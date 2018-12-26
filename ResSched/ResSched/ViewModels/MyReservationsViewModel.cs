using Ninject;
using ResSched.Mappers;
using ResSched.ObjModel;
using ResSched.Services;
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
            var ker = ((ResSched.App)Xamarin.Forms.Application.Current).Kernel;
            PreInit(ker.Get<IDataRetrievalService>());
        }
        /*public MyReservationsViewModel(IDataRetrievalService dataRetrievalService)
        {

        }*/

        private void PreInit(IDataRetrievalService dataRetrievalService)
        {

            //_dataService = dataRetrievalService;
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