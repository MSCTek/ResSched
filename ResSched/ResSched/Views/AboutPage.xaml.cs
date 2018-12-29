using ResSched.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ResSched.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        private AboutViewModel viewModel;

        public AboutPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new AboutViewModel();
        }
    }
}