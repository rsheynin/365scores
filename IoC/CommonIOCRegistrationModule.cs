using System.Data.Entity;
using Domain;
using Ninject.Modules;

namespace IoC
{
    public class CommonIOCRegistrationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<DbContext>().To<CompetionContext>().InSingletonScope();
        }
    }
}