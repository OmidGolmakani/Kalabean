using Kalabean.Domain.Entities;
using Kalabean.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kalabean.Infrastructure.Repositories
{
   public class ProductImageRepository : Repository<ProductImage>, IProductImageRepository
    {
        //private readonly DbFactory _dbFactory;
        public ProductImageRepository(DbFactory dbFactory) : base(dbFactory) { }

        public Task<ProductImage> GetById(int id, bool includeDeleted = false)
        {
            return this.DbSet
                .Where(pi => pi.Id == id && (includeDeleted || !pi.IsDeleted))
                .Include(pi => pi.Product)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<IQueryable<ProductImage>> Get(bool includeDeleted = false)
        {
            return this
                .List(pi => includeDeleted || !pi.IsDeleted)
                .Include(pi => pi.Product);
        }
    }
}
