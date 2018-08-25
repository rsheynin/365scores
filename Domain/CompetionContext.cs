using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CompetionContext : DbContext
    {
        public CompetionContext() : base("CompetionContext")
        {
        }

        public DbSet<Competition> Blogs { get; set; }
        
        public DbSet<Team> Teams { get; set; }
        
        public DbSet<TeamCompetition> TeamCompetitions { get; set; }

        public DbSet<League> Leagues { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
                    modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
