using System;
using System.Collections.Generic;
using Common;

namespace Domain
{
    public class Competition
    {
        public Competition()
        {
            Teams = new List<Team>();
        }

        public Competition(Competition competition)
        {
            Teams = competition.Teams;
            DateTime = competition.DateTime;
            Status = competition.Status;
            Country = competition.Country;
            LeagueType = competition.LeagueType;
            SportType = competition.SportType;
            Score = competition.Score;
        }

        public Competition(Competition competition, int id) : this(competition)
        {
            Teams = competition.Teams;
            DateTime = competition.DateTime;
            Status = competition.Status;
            Country = competition.Country;
            LeagueType = competition.LeagueType;
            SportType = competition.SportType;
            Score = competition.Score;
            CompetitionId = id;
        }

        public int CompetitionId { get; set; }

        public string Country { get; set; }

        public DateTime DateTime { get; set; }
        
        public string Score { get; set; }

        public virtual ICollection<Team> Teams { get; set; }
        
        public CompetitionStatus Status { get; set; }

        public SportType SportType { get; set; }

        public League LeagueType { get; set; }
    }
}