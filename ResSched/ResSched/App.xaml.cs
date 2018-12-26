using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.Identity.Client;
using Ninject;
using Ninject.Modules;
using ResSched.Modules;
using ResSched.Services;
using ResSched.ViewModels;
using ResSched.Views;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace ResSched
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        public static string AzureBackendUrl = "http://localhost:5000";

        public static bool UseMockDataStore = true;

        #region MSAL

        public static string AuthClientID = Config.MSALClientId;
        public static string[] AuthScopes = Config.MSALAuthScopes;
        public static PublicClientApplication PCA = null;
        public static UIParent UiParent { get; set; }

        #endregion MSAL

        #region AuthId

        public static string AuthUserEmail = string.Empty;
        public static string AuthUserName = string.Empty;

        #endregion AuthId

        //This empty constructor is used only for the ios XAML designer.
        public App()
        {
            InitializeComponent();
        }

        public App(params INinjectModule[] platformModules)
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

            // Register core services
            Kernel = new StandardKernel(new CoreModule());

            // Register platform specific services
            Kernel.Load(platformModules);

            //initialize the singleton
            var asyncconn = Kernel.Get<ISQLite>().GetAsyncConnection();
            var conn = Kernel.Get<ISQLite>().GetConnection();
            if (conn != null && asyncconn != null)
            {
                var db = Kernel.Get<IDatabase>();
                db.SetConnection(conn, asyncconn);
                db.CreateTables();
            }
            else
            {
                Debug.WriteLine("ERROR: SQLite Database could not be created.");
                throw new InvalidOperationException("ERROR: SQLite Database could not be created.");
            }

            //MainPage = new MainPage()
            var welcome = new WelcomePage();
            welcome.BindingContext = new WelcomeViewModel(Kernel.Get<IDataLoadService>());
            MainPage = welcome;

            
        }

        public void NavigateToMainPage()
        {
            MainPage = new MainPage();
        }

        public IKernel Kernel { get; set; }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            AppCenter.Start($"android={Config.AppCenterAndroid};uwp={Config.AppCenterUWP};ios={Config.AppCenteriOS}",
                  typeof(Analytics), typeof(Crashes));
        }
    }
}