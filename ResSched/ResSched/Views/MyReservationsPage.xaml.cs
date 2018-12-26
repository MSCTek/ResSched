using ResSched.ViewModels;

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

        public void Refresh()
        {
            if(BindingContext == null)
            {
                BindingContext = new MyReservationsViewModel();
            }

            if (this.BindingContext != null)
            {
                var vm = (MyReservationsViewModel)this.BindingContext;
                vm.Init();
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Refresh();
        }
    }
}