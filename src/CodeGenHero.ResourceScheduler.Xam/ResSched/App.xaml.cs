using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.Identity.Client;
using Ninject;
using Ninject.Modules;
using ResSched.Interfaces;
using ResSched.Modules;
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

        #region AuthId

        public static string AuthUserEmail = string.Empty;
        public static Guid AuthUserId = Guid.Empty;
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
            welcome.BindingContext = new WelcomeViewModel();
            MainPage = welcome;
        }

        public IKernel Kernel { get; set; }

        public void NavigateToMainPage()
        {
            MainPage = new MainPage();
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            Debug.WriteLine("ResSched is Resuming...");
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            Debug.WriteLine("ResSched is Sleeping...");
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            Debug.WriteLine("ResSched is Starting...");
            AppCenter.Start($"android={Config.AppCenterAndroid};uwp={Config.AppCenterUWP};ios={Config.AppCenteriOS}",
                  typeof(Analytics), typeof(Crashes));
        }
    }
}