using Ninject.Modules;
using ResSched.Interfaces;
using ResSched.Services;

namespace ResSched.Modules
{
    public class CoreModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDatabase>().To<Database.Database>().InSingletonScope();
            Bind<IDataRetrievalService>().To<DataRetrievalService>().InSingletonScope();
            Bind<IDataLoadService>().To<DataLoadSampleDataService>().InSingletonScope();
        }
    }
}