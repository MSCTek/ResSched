using ResSched.Models;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ResSched.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        private MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        public AboutPage()
        {
            InitializeComponent();
        }

        private void FacebookGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //Device.OpenUri(new Uri("https://www.facebook.com/foxbuildshop/"));
            Device.OpenUri(new Uri("https://www.facebook.com/sharer/sharer.php?u=https://fox.build/"));
        }

        private void PhoneGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                PhoneDialer.Open("16303449385");
            }
            catch (FeatureNotSupportedException ex)
            {
                Application.Current.MainPage.DisplayAlert("Error", "Sorry, the phone dialer is not supported on this device.", "OK");
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private void SlackGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Application.Current.MainPage.DisplayAlert(
                "Wait!", 
                "You must be a member to join our Slack Channel!", 
                "OK");
        }

        private void TwitterGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://twitter.com/fox_build"));
            //Device.OpenUri(new Uri( "https://twitter.com/intent/tweet?text=Home&url=https%3A%2F%2Ffox.build%2F&via=@fox_build"));
        }

        private void BackButton_Tapped(object sender, EventArgs e)
        {
            (RootPage.Master as MenuPage).TakeMeHere(MenuItemType.Browse);
        }
    }
}