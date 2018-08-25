using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Application.Excution
{
    public class CompetitionScanerManager : IExecution
    {
        private readonly IEnumerable<ICompetitionScanerProducerFactory> _scanerProducerFactories;
        private readonly ICompetitionScanerConsumerFactory _scanerConsumerFactory;

        public CompetitionScanerManager(
            IEnumerable<ICompetitionScanerProducerFactory> scanerProducerFactories,
            ICompetitionScanerConsumerFactory scanerConsumerFactory)
        {
            _scanerProducerFactories = scanerProducerFactories;
            _scanerConsumerFactory = scanerConsumerFactory;
        }
        public void Start()
        {
            Console.WriteLine("WebsiteScanerManager Running");
            var competitions = new BlockingCollection<Competition>();

            var producerTasks = RetrieveProducerTasks(competitions);

            var consumerTasks = RetrieveConsumerTasks(competitions);

            Task.WaitAll(producerTasks);
            competitions.CompleteAdding();
            Task.WaitAll(consumerTasks);
        }

        private Task[] RetrieveConsumerTasks(BlockingCollection<Competition> competitions)
        {
            var factoriesAmount = 4;//Get amount of factories from config or db ... 
            var consumerTasks = new Task[factoriesAmount];

            for (int i = 0; i < factoriesAmount; i++)
            {
                var scanerConsumer = _scanerConsumerFactory.Create(competitions);
                var consumerTask = Task.Run(() => scanerConsumer.Consume());
                consumerTasks[i] = consumerTask;
            }

            return consumerTasks;
        }

        private Task[] RetrieveProducerTasks(BlockingCollection<Competition> competitions)
        {
            var factoriesAmount = _scanerProducerFactories.Count();
            var producerTasks = new Task[factoriesAmount];

            for (int i = 0; i < factoriesAmount; i++)
            {
                var scanerProducer = _scanerProducerFactories.ElementAt(i).Create(competitions);
                var producerTask = Task.Run(() => scanerProducer.ProducerAsync());
                producerTasks[i] = producerTask;
            }

            return producerTasks;
        }
    }
}