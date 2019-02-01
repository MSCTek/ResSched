using CodeGenHero.ResourceScheduler.Xam.ModelObj.RS;
using ResSched.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ResSched.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditResourcesPage : ContentPage
    {
        private EditResourcesViewModel viewModel;

        public EditResourcesPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new EditResourcesViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext != null)
            {
                var vm = BindingContext as EditResourcesViewModel;
                await vm.InitVM();
            }
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as Resource;
            if (item == null)
                return;

            await Navigation.PushModalAsync(new EditResourceDetailsPage(new EditResourceDetailsViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }
    }
}