using System.Collections.Concurrent;
using Domain;

namespace Application.Excution
{
    public interface ICompetitionScanerProducerFactory
    {
        IScanerProducer Create(BlockingCollection<Competition> competitions);
    }
}