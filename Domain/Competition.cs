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
        public int CompetitionId { get; set; }

        public string Country { get; set; }

        public virtual IList<Team> Teams { get; set; }

        public DateTime DateTime { get; set; }
        
        public string Score { get; set; }
        
        public CompetitionStatus Status { get; set; }

        public SportType SportType { get; set; }

        public League LeagueType { get; set; }
    }
}