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

        public static PublicClientApplication PCA = null;
        public static string AuthClientID = "7fda6409-a86f-4e4f-8d59-288588dffa46";

        public static string[] AuthScopes = { "User.Read" };

        public static string AuthUserName = string.Empty;

        public static string AuthUserEmail = string.Empty;

        public static UIParent UiParent { get; set; }

        public App()
        {
            InitializeComponent();

            PCA = new PublicClientApplication(AuthClientID)
            {
                RedirectUri = $"msal{App.AuthClientID}://auth",
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
            AppCenter.Start("android=30c191aa-6efe-4434-8b3d-42f0a9e817b9;" +
                  "uwp=e682f484-a14d-46ea-8e08-714ff2b43dcc;" +
                  "ios=b3df61a0-5842-4342-a345-70f7d27f1575",
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
