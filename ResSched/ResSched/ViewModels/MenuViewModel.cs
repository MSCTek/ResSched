using ResSched.Models;
using System.Collections.ObjectModel;

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

            ObservableCollection<HomeMenuItem> items;

            if (App.AuthUserName == string.Empty)
            {
                items = new ObservableCollection<HomeMenuItem>
                {
                    new HomeMenuItem {Id = MenuItemType.Browse, Title="Browse" },
                    new HomeMenuItem {Id = MenuItemType.About, Title="About" },
                    new HomeMenuItem {Id = MenuItemType.Login, Title="Login" }
                };
            }
            else
            {
                items = new ObservableCollection<HomeMenuItem>
                {
                    new HomeMenuItem {Id = MenuItemType.Browse, Title="Browse" },
                    new HomeMenuItem {Id = MenuItemType.MyReservations, Title="My Reservations" },
                    new HomeMenuItem {Id = MenuItemType.About, Title="About" },
                    new HomeMenuItem {Id = MenuItemType.Login, Title="Login" }
                };
            }

            MenuItems = items;
        }
    }
}