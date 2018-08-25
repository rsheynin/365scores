using System;
using System.Collections.Generic;
using Application.Excution;
using Ninject;
using Ninject.Activation;
using Ninject.Modules;

namespace IoC
{
    public class WebSiteScanerIOCRegistrationModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IExecution>().To<CompetitionScanerManager>();
            
            RegisterScanerProducers();

            RegisterScannerConsumer();

        }

        private void RegisterScannerConsumer()
        {
            Kernel.Bind<ICompetitionScanerConsumerFactory>().To<WebsiteScanerConsumerFactory>();
            Kernel.Bind<IScanerConsumer>().To<WebsiteScanerConsumer>();

            //Kernel.Bind<IEnumerable<ICompetitionScanerConsumerFactory>>().ToMethod(RegisterScanerConsumerFactories);
        }

        //private IEnumerable<ICompetitionScanerConsumerFactory> RegisterScanerConsumerFactories(IContext arg)
        //{
        //    var factory = Kernel.Get<ICompetitionScanerConsumerFactory>();

        //    IEnumerable<ICompetitionScanerConsumerFactory> factories = new []
        //    {

        //    };
        //    return factories;
        //}

        private void RegisterScanerProducers()
        {
            //Kernel.Bind<ICompetitionScanerProducerFactory>().To<FutbolMeWebsiteScanerProducerFactory>();
            //Kernel.Bind<IScanerProducer>().To<FutbolMeWebsiteScanerProducer>();

            Kernel.Bind<ICompetitionScanerProducerFactory>().To<LiveScoresWebsiteScanerProducerFactory>();
            Kernel.Bind<IScanerProducer>().To<LiveScoresWebsiteScanerProducer>();

            Kernel.Bind<IWebHtmlReader>().To<WebHtmlReader>();

            Kernel.Bind<IEnumerable<ICompetitionScanerProducerFactory>>().ToMethod(RegisterScanerProducerFactories);
        }

        private IEnumerable<ICompetitionScanerProducerFactory> RegisterScanerProducerFactories(IContext arg)
        {
            var factories = Kernel.GetAll<ICompetitionScanerProducerFactory>();
            return factories;
        }
    }
}
