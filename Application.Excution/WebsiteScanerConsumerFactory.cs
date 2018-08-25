using System.Collections.Concurrent;
using Domain;

namespace Application.Excution
{
    public class WebsiteScanerConsumerFactory : ICompetitionScanerConsumerFactory
    {
        private readonly IRepository<Competition> _repository;

        public WebsiteScanerConsumerFactory(IRepository<Competition> repository)
        {
            _repository = repository;
        }

        public IScanerConsumer Create(BlockingCollection<Competition> scunnResults)
        {
            var consumer = new WebsiteScanerConsumer(scunnResults,_repository);
            return consumer;
        }
    }
}