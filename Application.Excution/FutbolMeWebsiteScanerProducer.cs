using System;
using System.Collections.Concurrent;
using Domain;

namespace Application.Excution
{
    public class FutbolMeWebsiteScanerProducer : IScanerProducer
    {
        private readonly BlockingCollection<Competition> _competitions;
        private readonly IWebHtmlReader _webHtmlReader;

        public FutbolMeWebsiteScanerProducer(BlockingCollection<Competition> competitions, IWebHtmlReader webHtmlReader)
        {
            _competitions = competitions;
            _webHtmlReader = webHtmlReader;
        }

        public async void Producer()
        {
            var htmlContent = _webHtmlReader.Read("https://futbolme.com/");
            throw new NotImplementedException();
        }
    }
}