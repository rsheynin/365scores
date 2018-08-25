using System;
using System.Data.Entity;
using System.Linq;

namespace Domain
{
    public class CompetionRepository : IRepository<Competition>
    {
        private readonly DbContext _context;

        public CompetionRepository(DbContext context)
        {
            _context = context;
        }

        public void AddOrUpdate(Competition obj)
        {
           

            throw new NotImplementedException();
        }
    }
}
