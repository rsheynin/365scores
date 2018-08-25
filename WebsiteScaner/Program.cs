using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Excution;
using Common;
using IoC;
using Ninject;
using Ninject.Modules;

namespace WebsiteScaner
{
    class Program
    {
        private static IExecution _engine;

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine(WebsiteScanerConst.SCANER_START, DateTime.Now);
                
                InitializeDependencyInjection();

                _engine.Start();

                Console.WriteLine(WebsiteScanerConst.SCANER_SUCCESS, DateTime.Now);
            }
            catch (Exception e)
            {
                //_logger.Error(e);
                Console.WriteLine(WebsiteScanerConst.SCANER_FAILURE, e.Message, DateTime.Now);
            }
        }

        private static void InitializeDependencyInjection()
        {
            var kernel = new StandardKernel();
            var modules = new List<INinjectModule>
            {
                new CommonIOCRegistrationModule(),
                new WebSiteScanerIOCRegistrationModule(),
            };

            kernel.Load(modules);
            
            _engine = kernel.Get<IExecution>();
        }

        
    }
}
