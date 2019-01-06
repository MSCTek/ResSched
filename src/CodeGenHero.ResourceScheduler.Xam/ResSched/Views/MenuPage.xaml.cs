using ResSched.Models;
using ResSched.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ResSched.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        //private List<HomeMenuItem> menuItems;

        public MenuPage()
        {
            InitializeComponent();

            Refresh();

            if (this.BindingContext != null)
            {
                var vm = (MenuViewModel)this.BindingContext;
                ListViewMenu.SelectedItem = vm.MenuItems[0];
            }
            ListViewMenu.ItemSelected += OnMenuItemSelected;
        }

        private MainPage RootPage { get => Application.Current.MainPage as MainPage; }

        public void Refresh()
        {
            if (this.BindingContext != null)
            {
                var vm = (MenuViewModel)this.BindingContext;
                vm.RefreshMenuItems();
            }
        }

        /*public void TakeMeHere(int pageNumber)
        {
            ListViewMenu.SelectedItem = menuItems[pageNumber];
        }

        public void TakeMeHere(MenuItemType page)
        {
            int id = (int)page;
            ListViewMenu.SelectedItem = menuItems[id];
        }*/

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Refresh();
        }

        private async void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var id = (int)((HomeMenuItem)e.SelectedItem).Id;
            await RootPage.NavigateFromMenu(id);
        }
    }
}