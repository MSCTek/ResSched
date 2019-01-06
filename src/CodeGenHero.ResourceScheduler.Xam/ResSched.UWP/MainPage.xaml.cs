using ResSched.UWP.Modules;

namespace ResSched.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new ResSched.App(new UWPPlatformModule()));
        }
    }
}