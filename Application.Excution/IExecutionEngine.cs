using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Excution
{
    /// <summary>
    /// Executor of specific execution context
    /// </summary>
    public interface IExecution
    {
        /// <summary>
        /// Start the execution
        /// </summary>
        void Start();
    }

    /// <summary>
    /// Represent a gateway to process inspection message
    /// </summary>
    public interface ICompetitionDispatcher
    {
        /// <summary>
        /// Dispatch the inspection results message to its target (e.g. REST server, local file system...)
        /// </summary>
        /// <param name="msg"></param>
        void Dispatch(Competition msg);
    }

    public class Competition
    {
        public int Id { get; set; }

        public SportType SportType { get; set; }

        public LeagueType LeagueType { get; set; }

        public IList<Team> Teams { get; set; }

        public DateTime DateTime { get; set; }
    }

    public class Team
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public enum LeagueType
    {
    }

    public enum SportType
    {

    }

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
                var producerTask = Task.Run(() => scanerProducer.Producer());
                producerTasks[i] = producerTask;
            }

            return producerTasks;
        }
    }

    //public class CompetitionScanerProducerManager : ICompetitionScanerProducerManager
    //{

    //}

    //public interface ICompetitionScanerProducerManager
    //{
    //}

    public interface ICompetitionScanerProducerFactory
    {
        IScanerProducer Create(BlockingCollection<Competition> competitions);
    }

    public class FutbolMeWebsiteScanerProducerFactory : ICompetitionScanerProducerFactory
    {
        public IScanerProducer Create(BlockingCollection<Competition> competitions)
        {
            var producer = new FutbolMeWebsiteScanerProducer(competitions);
            return producer;
        }
    }

    public class LiveScoresWebsiteScanerProducerFactory : ICompetitionScanerProducerFactory
    {
        public IScanerProducer Create(BlockingCollection<Competition> competitions)
        {
            var producer = new LiveScoresWebsiteScanerProducer(competitions);
            return producer;
        }
    }

   
    public class WebsiteScanerConsumerFactory : ICompetitionScanerConsumerFactory
    {
        public IScanerConsumer Create(BlockingCollection<Competition> scunnResults)
        {
            var consumer = new WebsiteScanerConsumer(scunnResults);
            return consumer;
        }
    }

    public interface ICompetitionScanerConsumerFactory
    {
        IScanerConsumer Create(BlockingCollection<Competition> scunnResults);
    }


    public class FutbolMeWebsiteScanerProducer : IScanerProducer
    {
        private readonly BlockingCollection<Competition> _competitions;
        private IWebSiteScaner _websiteScaner;
        private IWebsiteScanerCompetionHtmlCoverter _websiteScanerCompetionHtmlCoverter;

        public FutbolMeWebsiteScanerProducer(BlockingCollection<Competition> competitions)
        {
            _competitions = competitions;
        }

        public void Producer()
        {
            var htmlContent = _websiteScaner.Scun();

            foreach (var competitionHtml in htmlContent)
            {
                var competition = _websiteScanerCompetionHtmlCoverter.GetCometion(competitionHtml);

                _competitions.Add(competition);
            }
        }
    }

    public class LiveScoresWebsiteScanerProducer : IScanerProducer
    {
        private BlockingCollection<Competition> _competitions;

        public LiveScoresWebsiteScanerProducer(BlockingCollection<Competition> competitions)
        {
            _competitions = competitions;
        }

        public void Producer()
        {
            throw new NotImplementedException();
        }
    }

    public class WebsiteScanerConsumer : IScanerConsumer
    {
        private readonly BlockingCollection<Competition> _competitions;

        public WebsiteScanerConsumer(BlockingCollection<Competition> competitions)
        {
            _competitions = competitions;
        }

        public void Consume()
        {
            while (true)
            {
                try
                {
                    var scunnResult = _competitions.Take();
                    //_scanerResultDispatcher.Dispatch
                    
                    //Trade nextTransaction = _queue.Take();
                    //_staffLogs.ProcessTrade(nextTransaction);
                    //Console.WriteLine("Processing transaction from " + nextTransaction.Person.Name);
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
            }
        }
    }

    //public class LiveScoresWebsiteScanerConsumerFactory : ICompetitionScanerConsumerFactory
    //{
    //    public IScanerConsumer Create(BlockingCollection<Competition> scunnResults)
    //    {
    //        var consumer = new LiveScoresWebsiteScanerConsumer(scunnResults);
    //        return consumer;
    //    }
    //}
    //public class LiveScoresWebsiteScanerConsumer : IScanerConsumer
    //{
    //    private BlockingCollection<Competition> _competitions;

    //    public LiveScoresWebsiteScanerConsumer(BlockingCollection<Competition> competitions)
    //    {
    //        _competitions = competitions;
    //    }

    //    public void Consume()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    public interface IScanerConsumer
    {
        void Consume();
    }

    public interface IScanerProducer
    {
        void Producer();
    }

    

}
