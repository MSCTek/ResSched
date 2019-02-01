using CodeGenHero.ResourceScheduler.Xam.ModelObj.RS;
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

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as User;
            if (item == null)
                return;
            if (item.Email.ToLower() == "guest@guest.com")
            {
                await DisplayAlert("Sorry", "You can't edit the guest user!", "OK");
            }
            else
            {
                await Navigation.PushModalAsync(new EditUserDetailsPage(new EditUserDetailsViewModel(item)));
            }
            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }
    }
}