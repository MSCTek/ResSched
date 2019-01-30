using ResSched.Models;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace ResSched.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private ObservableCollection<HomeMenuItem> _menuItems;

        public MenuViewModel()
        {
            MenuItems = new ObservableCollection<HomeMenuItem>();
        }

        public ObservableCollection<HomeMenuItem> MenuItems
        {
            get { return _menuItems; }
            set { Set(nameof(MenuItems), ref _menuItems, value); }
        }

        internal void RefreshMenuItems()
        {
            MenuItems.Clear();

            ObservableCollection<HomeMenuItem> items = new ObservableCollection<HomeMenuItem>();

            if (App.AuthUserName == string.Empty || App.AuthUserEmail == "guest@guest.com")
            {
                items = new ObservableCollection<HomeMenuItem>
                {
                    new HomeMenuItem {Id = MenuItemType.Browse, Title="Browse Resources" },
                    new HomeMenuItem {Id = MenuItemType.About, Title="About" },
                    new HomeMenuItem {Id = MenuItemType.Events, Title="Events"},
                    new HomeMenuItem {Id = MenuItemType.Login, Title="Login" }
                };
            }
            else
            {
                items = new ObservableCollection<HomeMenuItem>
                    {
                        new HomeMenuItem {Id = MenuItemType.Browse, Title="Browse Resources" },
                        new HomeMenuItem {Id = MenuItemType.MyReservations, Title="My Reservations" },
                        new HomeMenuItem {Id = MenuItemType.About, Title="About" },
                        new HomeMenuItem {Id = MenuItemType.Events, Title="Events"},
                        new HomeMenuItem {Id = MenuItemType.Login, Title="Logout" }
                    };
                if (Device.RuntimePlatform == Device.UWP)
                {
                    items.Add(new HomeMenuItem { Id = MenuItemType.EditUsers, Title = "Edit Users" });
                    items.Add(new HomeMenuItem { Id = MenuItemType.EditResources, Title = "Edit Resources" });
                }
            }

            MenuItems = items;
        }
    }
}