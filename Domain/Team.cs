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


        public int CompetitionId { get; set; }
        public virtual Competition Competition { get; set; }
    }
}