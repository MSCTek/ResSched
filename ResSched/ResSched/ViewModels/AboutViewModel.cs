using GalaSoft.MvvmLight.Command;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ResSched.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
        }

        public RelayCommand FacebookCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    //Device.OpenUri(new Uri("https://www.facebook.com/foxbuildshop/"));
                    Device.OpenUri(new Uri("https://www.facebook.com/sharer/sharer.php?u=https://fox.build/"));
                });
            }
        }

        public RelayCommand PhoneCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    try
                    {
                        PhoneDialer.Open("16303449385");
                        Analytics.TrackEvent("Phone Call Attempted", new Dictionary<string, string> {
                        { "Where", "AboutUsPage-PhoneNumber-Tap" }
                    });
                    }
                    catch (FeatureNotSupportedException)
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
                });
            }
        }

        public RelayCommand SlackCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    Application.Current.MainPage.DisplayAlert(
                        "Wait!",
                        "You must be a member to join our Slack Channel!",
                        "OK");
                });
            }
        }

        public RelayCommand TestCrashCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    Crashes.GenerateTestCrash();
                    //int zero = 0;
                    //var badNUmber = zero / zero;
                });
            }
        }

        public RelayCommand TwitterCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    Device.OpenUri(new Uri("https://twitter.com/fox_build"));
                    //Device.OpenUri(new Uri( "https://twitter.com/intent/tweet?text=Home&url=https%3A%2F%2Ffox.build%2F&via=@fox_build"));
                });
            }
        }
    }
}