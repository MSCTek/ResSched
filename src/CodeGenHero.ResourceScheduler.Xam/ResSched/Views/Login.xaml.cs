using ResSched.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ResSched.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        private LoginViewModel viewModel;

        public Login()
        {
            InitializeComponent();
            BindingContext = viewModel = new LoginViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext != null)
            {
                var vm = BindingContext as LoginViewModel;
                await vm.InitVM();
            }
        }

        protected override bool OnBackButtonPressed()
        {
            // If you want to continue going back
            //base.OnBackButtonPressed();
            //return false;

            // If you want to stop the back button
            return true;
        }
    }
}