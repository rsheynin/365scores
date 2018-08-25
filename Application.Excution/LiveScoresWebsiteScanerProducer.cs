using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text.RegularExpressions;
using Common;
using Domain;
using HtmlAgilityPack;

namespace Application.Excution
{
    public class LiveScoresWebsiteScanerProducer : IScanerProducer
    {
        private readonly BlockingCollection<Competition> _competitions;
        private readonly IWebHtmlReader _webHtmlReader;
        //private IWebsiteScanerCompetionHtmlCoverter _websiteScanerCompetionHtmlCoverter;
        private const  string ITEM_URL = "http://www.livescores.com/";

        public LiveScoresWebsiteScanerProducer(BlockingCollection<Competition> competitions, IWebHtmlReader webHtmlReader)
        {
            _competitions = competitions;
            _webHtmlReader = webHtmlReader;
        }

        public async void ProducerAsync()
        {
            //todo: check for NullReferance exception 
            var htmlDoc = _webHtmlReader.Read(ITEM_URL);

            //Console.WriteLine(htmlContent.InnerText);
            var htmlContent = htmlDoc.DocumentNode.SelectNodes("//div[@class='content']//div[contains(@class,'row row-tall mt4') or contains(@class,'row-gray')]");

            var league = new League();
            var date = new DateTime();

            foreach (var html in htmlContent)
            {
                //var competition = _websiteScanerCompetionHtmlCoverter.GetCometion(html,league,date);
                var isCopetitionNode = html.Attributes["class"].Value.Contains("row-gray");
                var htmlNodeCollection = html.ChildNodes;
                if (isCopetitionNode)
                {
                    var competition = new Competition();
                    competition.DateTime = date;
                    competition.LeagueType = league;
                    competition.Country = league.Country;
                    
                    GetCompetitionTime(htmlNodeCollection, competition);

                    GetCompetitionTeams(competition, htmlNodeCollection);

                    Notify(competition);

                    var tryAddCompetition = _competitions.TryAdd(competition);
                }
                else
                {
                    //todo: check for NullReferance exception 
                    var leagueHtmlDescendantsCollection = html.Descendants().ElementAt(1).ChildNodes;

                    var dateString = leagueHtmlDescendantsCollection[3].InnerText;
                    date = Convert.ToDateTime(dateString);

                    var t = leagueHtmlDescendantsCollection[1].ChildNodes;

                    league.Country = t[1].InnerText;
                    league.LeagueType = t[3].InnerText;

                    NotifyLeague(league, date);

                }
            }
        }

        private static void GetCompetitionTeams(Competition competition, HtmlNodeCollection htmlNodeCollection)
        {
            competition.Score = htmlNodeCollection[5].InnerText;
            competition.Teams.Add(new Team(htmlNodeCollection[3].InnerText));
            competition.Teams.Add(new Team(htmlNodeCollection[7].InnerText));
        }

        private static void GetCompetitionTime(HtmlNodeCollection htmlNodeCollection, Competition competition)
        {
            var time = htmlNodeCollection[1].InnerText;


            if (string.Equals(time, " FT "))
            {
                competition.Status = CompetitionStatus.GameOver;
            }
            else if (time.Contains("&#x27;"))
            {
                competition.Status = CompetitionStatus.Live;
            }
            else if (time.Contains(" Postp. "))
            {
                competition.Status = CompetitionStatus.LimitedCoverage;
            }
            else if (time.Contains(" HT "))
            {
                competition.Status = CompetitionStatus.TimeOut;
            }
            else if (time.Contains(":"))
            {
                var match = Regex.Match(time, @"(\d+)[:.\/](\d+)");
                if (match.Success)
                {
                    competition.DateTime = competition.DateTime
                        .AddHours(Convert.ToDouble(match.Groups[1].Value))
                        .AddMinutes(Convert.ToDouble(match.Groups[2].Value));
                }
            }
        }

        private void NotifyLeague(League league, DateTime date)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("###############################################");
           
            Console.WriteLine(league.Country + " - " + league.LeagueType + " - " + date);
        }

        private void Notify(Competition competition)
        {

            foreach (var competitionTeam in competition.Teams)
            {
                Console.Write(competitionTeam.Name + " ");
            }

            switch (competition.Status)
            {
                case CompetitionStatus.NotStarted:
                    Console.WriteLine(competition.Score + " - " + competition.DateTime);
                    break;
                case CompetitionStatus.Live:
                case CompetitionStatus.GameOver:
                case CompetitionStatus.LimitedCoverage:
                case CompetitionStatus.TimeOut:
                    Console.WriteLine(competition.Score + "-  "+competition.Status + " - " + competition.DateTime);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}