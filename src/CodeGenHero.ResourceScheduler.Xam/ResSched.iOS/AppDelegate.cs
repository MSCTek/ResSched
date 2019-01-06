using Foundation;
using Microsoft.Identity.Client;
using ResSched.Helpers;
using ResSched.iOS.Modules;
using ResSched.iOS.Services;
using System;
using UIKit;
using Xamarin.Forms;

namespace ResSched.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public static Action BackgroundSessionCompletionHandler;

        private IOSRunQueuedUpdateService myiOSUploadDataService;

        //
        // This method is invoked when the application has loaded and is ready to run. In this
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            //Xamarin.Auth
            //global::Xamarin.Auth.Presenters.XamarinIOS.AuthenticationConfiguration.Init();

            // Initialize Azure Mobile Apps
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

            SubscribeToMessages();

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App(new IOSPlatformModule()));

            return base.FinishedLaunching(app, options);
        }

        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(url);
            return true;
        }

        private void SubscribeToMessages()
        {
            MessagingCenter.Subscribe<StartUploadDataMessage>(this, "StartUploadDataMessage", async message =>
            {
                myiOSUploadDataService = new IOSRunQueuedUpdateService();
                await myiOSUploadDataService.StartAsync();
            });

            MessagingCenter.Subscribe<StopUploadDataMessage>(this, "StopUploadDataMessage", message =>
            {
                myiOSUploadDataService.Stop();
            });
        }
    }
}