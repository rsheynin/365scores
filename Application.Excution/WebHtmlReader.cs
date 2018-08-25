using System;
using HtmlAgilityPack;

namespace Application.Excution
{
    public class WebHtmlReader : IWebHtmlReader
    {
        public HtmlDocument Read(string htmlUrl)
        {
            try
            {
                var web = new HtmlWeb();
                var doc = web.Load(htmlUrl);

                return doc;

            }
            catch (UriFormatException uex)
            {
                Console.WriteLine("There was an error in the format of the url: " + htmlUrl, uex);
                throw;
            }
            catch (System.Net.WebException wex)
            {
                Console.WriteLine("There was an error connecting to the url: " + htmlUrl, wex);
                throw;
            }

        }
    }
}