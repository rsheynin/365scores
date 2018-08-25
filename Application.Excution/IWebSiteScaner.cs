using HtmlAgilityPack;

namespace Application.Excution
{
    public interface IWebHtmlReader
    {
        HtmlDocument Read(string htmlUrl);
    }
}