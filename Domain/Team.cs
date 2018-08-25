using System.Collections.Generic;

namespace Domain
{
    public class Team
    {
        public Team(string name)
        {
            Name = name;
        }

        public int TeamId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<TeamCompetition> TeamCompetition { get; set; }
    }
}