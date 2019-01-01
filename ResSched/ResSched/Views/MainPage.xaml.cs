using ResSched.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ResSched.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        private Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();

        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;
            //why are we doing this?
            MenuPages.Add((int)MenuItemType.Login, (NavigationPage)Detail);
            this.IsPresentedChanged += OnPresentedChanged;
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.Browse:
                        MenuPages.Add(id, new NavigationPage(new BrowseResourcesPage()));
                        break;

                    case (int)MenuItemType.About:
                        MenuPages.Add(id, new NavigationPage(new AboutPage()));
                        break;

                    case (int)MenuItemType.Login:
                        MenuPages.Add(id, new NavigationPage(new Login()));
                        break;

                    case (int)MenuItemType.MyReservations:
                        MenuPages.Add(id, new NavigationPage(new MyReservationsPage()));
                        break;

                    case (int)MenuItemType.Events:
                        MenuPages.Add(id, new NavigationPage(new EventsPage()));
                        break;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }

        private void OnPresentedChanged(object sender, EventArgs e)
        {
            ((MenuPage)this.Master).Refresh();
        }
    }
}