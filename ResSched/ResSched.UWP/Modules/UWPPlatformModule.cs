using Ninject.Modules;
using ResSched.Interfaces;
using ResSched.Services;
using ResSched.UWP.Services;

namespace ResSched.UWP.Modules
{
    public class UWPPlatformModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISQLite>().To<UWPSQLite>().InSingletonScope();
        }
    }
}