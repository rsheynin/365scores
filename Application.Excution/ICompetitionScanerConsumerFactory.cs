using System.Collections.Concurrent;
using Domain;

namespace Application.Excution
{
    public interface ICompetitionScanerConsumerFactory
    {
        IScanerConsumer Create(BlockingCollection<Competition> scunnResults);
    }
}