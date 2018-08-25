namespace Domain
{
    public class TeamCompetition
    {
        public int TeamCompetitionId { get; set; }

        public int TeamId { get; set; }
        
        public int CompetitionId { get; set; }

        public virtual Competition Competition { get; set; }
        public virtual Team Team { get; set; }
    }
}