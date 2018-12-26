using Ninject.Modules;
using ResSched.Droid.Services;
using ResSched.Interfaces;

namespace ResSched.Droid.Modules
{
    public class DroidPlatformModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISQLite>().To<DroidSQLite>().InSingletonScope();
        }
    }
}