using CodeGenHero.ResourceScheduler.Xam.ModelObj.RS;
using ResSched.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ResSched.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BrowseResourcesPage : ContentPage
    {
        private BrowseResourcesViewModel viewModel;

        public BrowseResourcesPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new BrowseResourcesViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext != null)
            {
                var vm = BindingContext as BrowseResourcesViewModel;
                await vm.InitVM();
            }
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Resource;
            if (item == null)
                return;

            await Navigation.PushModalAsync(new ResourceDetailPage(new ResourceDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }
    }
}