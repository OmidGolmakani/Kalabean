using Kalabean.Domain.Entities;
using Kalabean.Domain.Repositories;
using Kalabean.Domain.Requests.Product;
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

        public Task<Product> GetById(long id, bool includeDeleted = false)
        {
            return this.DbSet
                .Where(p => p.Id == id && (includeDeleted || !p.IsDeleted))
                .Include(p => p.Category)
                .Include(p => p.Store)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<IQueryable<Product>> Get(GetProductsRequest request, bool includeDeleted = false)
        {
            return this
                .List(p => (includeDeleted || !p.IsDeleted) &&
                           (string.IsNullOrEmpty(request.ProductName) || p.ProductName.Contains(request.ProductName) &&
                           (request.CategoryId == null || p.CategoryId == request.CategoryId) &&
                           (request.StoreId == null || p.StoreId == request.StoreId))
                )
                .Skip(request.PageSize * request.PageIndex).Take(request.PageSize)
                .Include(pi => pi.Category)
                .Include(pi => pi.Store);
        }

        public async Task<long> Count(GetProductsRequest request, bool includeDeleted = false)
        {
            return this.List(p => (includeDeleted || !p.IsDeleted) &&
                           (string.IsNullOrEmpty(request.ProductName) || p.ProductName.Contains(request.ProductName) &&
                           (request.CategoryId == null || p.CategoryId == request.CategoryId) &&
                           (request.StoreId == null || p.StoreId == request.StoreId))
                ).Count();
        }
    }
}
