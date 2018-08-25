using System.Collections.Concurrent;
using Domain;

namespace Application.Excution
{
    public class LiveScoresWebsiteScanerProducerFactory : ICompetitionScanerProducerFactory
    {
        private readonly IWebHtmlReader _webHtmlReader;

        public LiveScoresWebsiteScanerProducerFactory(IWebHtmlReader webHtmlReader)
        {
            _webHtmlReader = webHtmlReader;
        }

        public IScanerProducer Create(BlockingCollection<Competition> competitions)
        {
            var producer = new LiveScoresWebsiteScanerProducer(competitions, _webHtmlReader);
            return producer;
        }
    }
}