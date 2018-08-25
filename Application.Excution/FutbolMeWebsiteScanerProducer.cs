using System;
using System.Collections.Concurrent;
using Domain;

namespace Application.Excution
{
    public class FutbolMeWebsiteScanerProducer : IScanerProducer
    {
        private readonly BlockingCollection<Competition> _competitions;
        private readonly IWebHtmlReader _webHtmlReader;
        //private IWebsiteScanerCompetionHtmlCoverter _websiteScanerCompetionHtmlCoverter;

        public FutbolMeWebsiteScanerProducer(BlockingCollection<Competition> competitions, IWebHtmlReader webHtmlReader)
        {
            _competitions = competitions;
            _webHtmlReader = webHtmlReader;
        }

        public async void ProducerAsync()
        {
            var htmlContent = _webHtmlReader.Read("https://futbolme.com/");

            //var league = new League();
            //var date = new DateTime();
            //foreach (var html in htmlContent)
            //{
            //    var isCopetitionNode = html.Attributes["class"].Value.Contains("row-gray");
            //    if (isCopetitionNode)
            //    {
            //        //var competition = _websiteScanerCompetionHtmlCoverter.GetCometion(selectSingleNode);
            //    }
            //    else
            //    {
            //        league.Country = html.SelectSingleNode("//div[@class='clear']//div[@class='left']//a//strong").InnerText;
                    
            //        //date = html.SelectSingleNode("//div[@class='content']//div[contains(@class,'row row-tall mt4') or contains(@class,'row-gray')]").InnerText;
            //    }


                

            //    //_competitions.Add(competition);
            //}
        }
    }
}