using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ResSched.Services;
using ResSched.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.Identity.Client;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ResSched
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        public static string AzureBackendUrl = "http://localhost:5000";
        public static bool UseMockDataStore = true;

        #region MSAL
        public static PublicClientApplication PCA = null;
        public static string AuthClientID = Config.MSALClientId;
        public static string[] AuthScopes = Config.MSALAuthScopes;
        public static UIParent UiParent { get; set; }
        #endregion

        #region AuthId
        public static string AuthUserName = string.Empty;
        public static string AuthUserEmail = string.Empty;
        #endregion



        public App()
        {
            InitializeComponent();

            //MSAL
            PCA = new PublicClientApplication(AuthClientID)
            {
                RedirectUri = Config.MSALRedirectUri,
            };

            if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<AzureDataStore>();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            AppCenter.Start($"android={Config.AppCenterAndroid};uwp={Config.AppCenterUWP};ios={Config.AppCenteriOS}",
                  typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
