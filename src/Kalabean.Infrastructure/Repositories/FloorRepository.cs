using Kalabean.Domain.Entities;
using Kalabean.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Kalabean.Infrastructure.Repositories
{
    public class FloorRepository : Repository<Floor>, IFloorRepository
    {
        private readonly DbFactory _dbFactory;
        public FloorRepository(DbFactory dbFactory) : base(dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public Task<Floor> GetById(int id, bool includeDeleted = false)
        {
            return this.DbSet
                .Where(c => c.Id == id && (includeDeleted || !c.IsDeleted))
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<IQueryable<Floor>> Get(bool includeDeleted = false)
        {
            return this
                .List(c => includeDeleted || !c.IsDeleted);
        }
    }
}
