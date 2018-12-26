using ResSched.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ResSched.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyReservationsPage : ContentPage
    {
        public MyReservationsPage()
        {
            InitializeComponent();
        }

        public async Task Refresh()
        {
            if (BindingContext == null)
            {
                BindingContext = new MyReservationsViewModel();
            }

            if (this.BindingContext != null)
            {
                var vm = (MyReservationsViewModel)this.BindingContext;
                await vm.Init();
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Refresh();
        }
    }
}