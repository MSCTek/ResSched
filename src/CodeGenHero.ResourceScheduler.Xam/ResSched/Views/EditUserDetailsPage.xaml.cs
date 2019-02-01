using ResSched.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ResSched.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditUserDetailsPage : ContentPage
    {
        private EditUserDetailsViewModel viewModel;

        public EditUserDetailsPage(EditUserDetailsViewModel _viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel != null)
            {
                await viewModel.Refresh();
            }
        }

        private void Cancel_OnClicked(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void Save_OnClicked(object sender, System.EventArgs e)
        {
        }
    }
}