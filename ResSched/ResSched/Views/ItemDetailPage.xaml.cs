using ResSched.Models;
using ResSched.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ResSched.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        private ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel != null)
            {
                await viewModel.Refresh();
            }
        }

        private void CanBook_OnClicked(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(App.AuthUserEmail))
            {
                //users that are not logged in cannot reserve resrouces
                Application.Current.MainPage.DisplayAlert("Sorry", "Please Login to reserve a resource!", "OK");
            }
            else
            {
                //create a new resource schedule
                Navigation.PushModalAsync(new ModReservationPage(viewModel.SelectedDate, viewModel.Resource));
            }
        }

        private void Cancel_OnClicked(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void EditReservation_Tapped(object sender, System.EventArgs e)
        {
            var hourlySchedule = (HourlySchedule)((Label)sender).Parent.BindingContext;
            //create a new resource schedule
            Navigation.PushModalAsync(new ModReservationPage(hourlySchedule));
        }
    }
}