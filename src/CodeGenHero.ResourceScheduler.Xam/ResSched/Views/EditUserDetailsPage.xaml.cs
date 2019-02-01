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

        private void Cancel_OnClicked(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private async void Save_OnClicked(object sender, System.EventArgs e)
        {
            if (viewModel != null)
            {
                var result = await viewModel.SaveAsync();
                if (result)
                {
                    await DisplayAlert("Success", "User was updated", "OK");
                }
                else
                {
                    await DisplayAlert("Failure", "User was not updated! Please check the logs for error info.", "OK");
                }

                Navigation.PopModalAsync();
            }
        }
    }
}