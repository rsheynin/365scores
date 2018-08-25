using System;
using System.Collections.Concurrent;
using Domain;

namespace Application.Excution
{
    public class WebsiteScanerConsumer : IScanerConsumer
    {
        private readonly BlockingCollection<Competition> _competitions;
        private readonly IReposytory _repository;

        public WebsiteScanerConsumer(
            BlockingCollection<Competition> competitions, 
            IReposytory reposytory)
        {
            _competitions = competitions;
            _repository = reposytory;
        }

        public void Consume()
        {
            while (true)
            {
                try
                {
                    var competition = _competitions.Take();
                    _repository.AddOrUpdate(competition);
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
            }
        }
    }
}