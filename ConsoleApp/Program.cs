using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {


        }

        public async Task<HtmlNodeCollection> Index()
        {
            HttpClient hc = new HttpClient();
            HttpResponseMessage result = await hc.GetAsync($"http://{HttpContext.Request.Host}/page.html");

            Stream stream = await result.Content.ReadAsStreamAsync();

            HtmlDocument doc = new HtmlDocument();

            doc.Load(stream);

            HtmlNodeCollection links = doc.DocumentNode.SelectNodes("//a[@href]");//the parameter is use xpath see: https://www.w3schools.com/xml/xml_xpath.asp 

            return links;
        }
    }
}
