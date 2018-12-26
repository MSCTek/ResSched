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
    }
}