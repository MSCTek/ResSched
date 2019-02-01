using ResSched.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ResSched.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditResourceDetailsPage : ContentPage
    {
        private EditResourceDetailsViewModel viewModel;

        public EditResourceDetailsPage(EditResourceDetailsViewModel _viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = _viewModel;
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