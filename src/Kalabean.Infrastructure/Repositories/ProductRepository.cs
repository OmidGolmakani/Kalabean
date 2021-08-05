using Kalabean.Domain.Entities;
using Kalabean.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kalabean.Infrastructure.Repositories
{
   public class ProductRepository : Repository<Product>, IProductRepository
    {
        //private readonly DbFactory _dbFactory;
        public ProductRepository(DbFactory dbFactory) : base(dbFactory) { }

        public Task<Product> GetById(int id, bool includeDeleted = false)
        {
            return this.DbSet
                .Where(c => c.Id == id && (includeDeleted || !c.IsDeleted))
                .Include(c => c.AccessRule)
                .Include(c => c.Category)
                .Include(c => c.Store)
                .Include(c => c.AccessRule)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<IQueryable<Product>> Get(bool includeDeleted = false)
        {
            return this
                .List(c => includeDeleted || !c.IsDeleted)
                 .Include(c => c.AccessRule)
                .Include(c => c.Category)
                .Include(c => c.Store)
                .Include(c => c.AccessRule);
        }
    }
}
