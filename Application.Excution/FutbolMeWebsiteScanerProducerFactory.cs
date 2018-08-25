using System.Collections.Concurrent;
using Domain;

namespace Application.Excution
{
    public class FutbolMeWebsiteScanerProducerFactory : ICompetitionScanerProducerFactory
    {
        private readonly IWebHtmlReader _webHtmlReader;

        public FutbolMeWebsiteScanerProducerFactory(IWebHtmlReader webHtmlReader)
        {
            _webHtmlReader = webHtmlReader;
        }
        public IScanerProducer Create(BlockingCollection<Competition> competitions)
        {
            var producer = new FutbolMeWebsiteScanerProducer(competitions,_webHtmlReader);
            return producer;
        }
    }
}