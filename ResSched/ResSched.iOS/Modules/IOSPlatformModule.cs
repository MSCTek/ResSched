using Ninject.Modules;
using ResSched.Interfaces;
using ResSched.iOS.Services;

namespace ResSched.iOS.Modules
{
    public class IOSPlatformModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISQLite>().To<IOSSQLite>().InSingletonScope();
        }
    }
}