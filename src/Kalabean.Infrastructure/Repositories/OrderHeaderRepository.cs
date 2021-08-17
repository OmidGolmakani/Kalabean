using Kalabean.Domain.Entities;
using Kalabean.Domain.Repositories;
using Kalabean.Domain.Requests.OrderHeader;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kalabean.Infrastructure.Repositories
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        //private readonly DbFactory _dbFactory;
        public OrderHeaderRepository(DbFactory dbFactory) : base(dbFactory) { }

        public Task<OrderHeader> GetById(long id, bool includeDeleted = false)
        {
            return this.DbSet
                .Where(p => p.Id == id && (includeDeleted || !p.IsDeleted))
                .Include(pi => pi.Store)
                .Include(p => p.OrderDetails)
                .ThenInclude(p => p.Product)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<IQueryable<OrderHeader>> Get(GetOrdersRequest request, bool includeDeleted = false)
        {
            return this
                .List(
                p => (includeDeleted || !p.IsDeleted) &&
                (request.StoreId == null || p.StoreId == request.StoreId) &&
                (request.ProductId == null || p.OrderDetails.Any(d => d.ProductId == request.ProductId)))
                .Include(pi => pi.Store)
                .Include(p => p.OrderDetails)
                .ThenInclude(p => p.Product);
        }

        public async Task<long> Count(GetOrdersRequest request, bool includeDeleted = false)
        {
            return this.List(
                p => (includeDeleted || !p.IsDeleted) &&
                (request.StoreId == null || p.StoreId == request.StoreId) &&
                (request.ProductId == null || p.OrderDetails.Any(d => d.ProductId == request.ProductId))).Count();
        }
    }
}
