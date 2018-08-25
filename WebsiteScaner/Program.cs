using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Excution;
using Common;
using IoC;
using Ninject;
using Ninject.Modules;

namespace WebsiteScaner
{
    class Program
    {
        private static IExecution _engine;

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine(WebsiteScanerConst.SCANER_START, DateTime.Now);
                
                InitializeDependencyInjection();

                _engine.Start();

                Console.WriteLine(WebsiteScanerConst.SCANER_SUCCESS, DateTime.Now);
            }
            catch (Exception e)
            {
                //_logger.Error(e);
                Console.WriteLine(WebsiteScanerConst.SCANER_FAILURE, e.Message, DateTime.Now);
            }
        }

        private static void InitializeDependencyInjection()
        {
            var kernel = new StandardKernel();
            var modules = new List<INinjectModule>
            {
                new WebSiteScanerIOCRegistrationModule(),

            };

            kernel.Load(modules);
            
            _engine = kernel.Get<IExecution>();
        }

        //public async Task<HtmlNodeCollection> Index()
        //{
        //    HttpClient hc = new HttpClient();
        //    HttpResponseMessage result = await hc.GetAsync($"http://{HttpContext.Request.Host}/page.html");

        //    Stream stream = await result.Content.ReadAsStreamAsync();

        //    HtmlDocument doc = new HtmlDocument();

        //    doc.Load(stream);

        //    HtmlNodeCollection links = doc.DocumentNode.SelectNodes("//a[@href]");//the parameter is use xpath see: https://www.w3schools.com/xml/xml_xpath.asp 

        //    return links;
        //}
    }
}
