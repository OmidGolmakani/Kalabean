using Kalabean.Domain.Entities;
using Kalabean.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Kalabean.Infrastructure.Repositories
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        private readonly DbFactory _dbFactory;
        public CityRepository(DbFactory dbFactory) : base(dbFactory)
        {
            _dbFactory = dbFactory;
        }
        public Task<City> GetById(int id, bool includeDeleted = false)
        {
            return this.DbSet
                .Where(c => c.Id == id && (includeDeleted || !c.IsDeleted))
                .Include(c => c.AccessRule)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}
