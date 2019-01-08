using CodeGenHero.ResourceScheduler.Xam.ModelObj.RS;
using ResSched.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ResSched.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyReservationsPage : ContentPage
    {
        private MyReservationsViewModel viewModel;

        public MyReservationsPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new MyReservationsViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (this.BindingContext != null)
            {
                var vm = (MyReservationsViewModel)this.BindingContext;
                await vm.InitVM();
            }
        }

        private async void CancelReservationButton_Clicked(object sender, System.EventArgs e)
        {
            var answer = await DisplayAlert("Please Confirm", "Are you sure you want to delete this reservation?", "Yes", "No");
            if (answer)
            {
                var id = ((ResourceSchedule)(((Button)sender).BindingContext)).Id;
                ((MyReservationsViewModel)this.BindingContext).OnCancelReservation(id);
            }
        }
    }
}