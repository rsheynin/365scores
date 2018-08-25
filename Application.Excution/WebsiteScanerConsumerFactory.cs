using System.Collections.Concurrent;
using Domain;

namespace Application.Excution
{
    public class WebsiteScanerConsumerFactory : ICompetitionScanerConsumerFactory
    {
        public IScanerConsumer Create(BlockingCollection<Competition> scunnResults)
        {
            var consumer = new WebsiteScanerConsumer(scunnResults);
            return consumer;
        }
    }
}