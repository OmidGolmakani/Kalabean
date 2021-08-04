using Kalabean.Domain.Entities;
using Kalabean.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kalabean.Infrastructure.Repositories
{
   public class ShoppingCenterRepository : Repository<ShoppingCenter>, IShoppingCenterRepository
    {
        //private readonly DbFactory _dbFactory;
        public ShoppingCenterRepository(DbFactory dbFactory) : base(dbFactory) { }

        public Task<ShoppingCenter> GetById(int id, bool includeDeleted = false)
        {
            return this.DbSet
                .Where(c => c.Id == id && (includeDeleted || !c.IsDeleted))
                .Include(c => c.Type)
                .Include(c => c.City)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<IQueryable<ShoppingCenter>> Get(bool includeDeleted = false)
        {
            return this
                .List(c => includeDeleted || !c.IsDeleted)
                .Include(c => c.Type)
                .Include(c => c.City);
        }
    }
}
