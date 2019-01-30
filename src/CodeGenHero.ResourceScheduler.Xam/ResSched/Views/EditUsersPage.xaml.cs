using ResSched.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ResSched.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditUsersPage : ContentPage
    {
        private EditUsersViewModel viewModel;

        public EditUsersPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new EditUsersViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext != null)
            {
                var vm = BindingContext as EditUsersViewModel;
                await vm.InitVM();
            }
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
        }
    }
}