using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Microsoft.Identity.Client;
using PIMobile.Droid.Services;
using Plugin.CurrentActivity;
using ResSched.Droid.Modules;
using ResSched.Helpers;
using Xamarin.Forms;

//using Microsoft.WindowsAzure.MobileServices;

namespace ResSched.Droid
{
    [Activity(Label = "ResSched", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        //public static MobileServiceClient MobileService = new MobileServiceClient("https://ressched.azurewebsites.net");

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(requestCode, resultCode, data);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            // Initialize Azure Mobile Apps
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

            //for Xamarin.Auth
            //global::Xamarin.Auth.Presenters.XamarinAndroid.AuthenticationConfiguration.Init(this, savedInstanceState);

            //for CrossCurrentActivity Plugin
            CrossCurrentActivity.Current.Init(this, savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App(new DroidPlatformModule()));

            //for MSAL
            //App.UiParent = new UIParent(Xamarin.Forms.Forms.Context as Activity);

            //for safe backgrounding
            SubscribeToMessages();
        }

        private void SubscribeToMessages()
        {
            //implement safe backgrounding
            MessagingCenter.Subscribe<StartUploadDataMessage>(this, "StartUploadDataMessage", message =>
            {
                var intent = new Intent(this, typeof(DroidRunQueuedUpdateService));
                StartService(intent);
            });

            MessagingCenter.Subscribe<StopUploadDataMessage>(this, "StopUploadDataMessage", message =>
            {
                var intent = new Intent(this, typeof(StopUploadDataMessage));
                StopService(intent);
            });
        }
    }
}