using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ResSched.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        private MainPage RootPage { get => Application.Current.MainPage as MainPage; }

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

                Analytics.TrackEvent("Phone Call Attempted", new Dictionary<string, string> {
                        { "Where", "AboutUsPage-PhoneNumber-Tap" }
                    });
            }
            catch (FeatureNotSupportedException ex)
            {
                Application.Current.MainPage.DisplayAlert("Error", "Sorry, the phone dialer is not supported on this device.", "OK");
                Analytics.TrackEvent("Phone Call Attempted", new Dictionary<string, string> {
                        { "Where", "AboutUsPage-PhoneNumber-Tap" },
                        { "Error", "Phone dialer was not supported on device."}
                    });
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                Crashes.TrackError(ex, new Dictionary<string, string>{
                            { "Where", "AboutUsPage-PhoneNumber-Tap" },
                            { "Error", ex.Message }
                        });
            }
        }

        private void SlackGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Application.Current.MainPage.DisplayAlert(
                "Wait!",
                "You must be a member to join our Slack Channel!",
                "OK");
        }

        private void TestCrashButton_Clicked(object sender, EventArgs e)
        {
            //Crashes.GenerateTestCrash();

            int zero = 0;
            var badNUmber = zero / zero;
        }

        private void TwitterGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://twitter.com/fox_build"));
            //Device.OpenUri(new Uri( "https://twitter.com/intent/tweet?text=Home&url=https%3A%2F%2Ffox.build%2F&via=@fox_build"));
        }
    }
}