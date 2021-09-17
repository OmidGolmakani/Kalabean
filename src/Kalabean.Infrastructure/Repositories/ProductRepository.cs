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
                .Include(p => p.ProductImages)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<IQueryable<Product>> Get(GetProductsRequest request, bool includeDeleted = false)
        {
            return this
                .List(p => (includeDeleted || !p.IsDeleted) &&
                           (string.IsNullOrEmpty(request.ProductName) || p.ProductName.Contains(request.ProductName)) &&
                           (request.CategoryId == null || p.CategoryId == request.CategoryId) &&
                           (!request.StoreId.HasValue || p.StoreId == request.StoreId) &&
                           (!request.IsEnabled.HasValue || p.IsEnabled == request.IsEnabled) &&
                           (request.Ids.Length==0 || request.Ids.Contains(p.Id))
                )
                .Skip(request.PageSize * request.PageIndex).Take(request.PageSize)
                .Include(pi => pi.Category)
                .Include(pi => pi.Store)
                .Include(pi => pi.ProductImages);
        }

        public async Task<int> Count(GetProductsRequest request, bool includeDeleted = false)
        {
            return this.List(p => (includeDeleted || !p.IsDeleted) &&
                           (string.IsNullOrEmpty(request.ProductName) || p.ProductName.Contains(request.ProductName)) &&
                           (request.CategoryId == null || p.CategoryId == request.CategoryId) &&
                           (!request.StoreId.HasValue || p.StoreId == request.StoreId) &&
                           (!request.IsEnabled.HasValue || p.IsEnabled == request.IsEnabled) &&
                           (request.Ids.Length == 0 || request.Ids.Contains(p.Id))
                ).Count();
        }
    }
}
