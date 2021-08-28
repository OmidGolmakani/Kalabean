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
                (request.OrderNum == null || p.OrderNum == request.OrderNum) &&
                (request.OrderFrom == null || request.OrderTo == null || 
                (p.CreatedDate >= request.OrderFrom && p.CreatedDate <= request.OrderTo)) &&
                (request.PaymentFrom == null || request.PaymentTo == null ||
                (p.PaymenyDate >= request.PaymentFrom && p.PaymenyDate <= request.PaymentTo)) &&
                (request.Type == "issued" || request.FromUserId == null || p.FromUserId == request.FromUserId) &&
                (request.Type == "received" || request.ToUserId == null || p.ToUserId == request.ToUserId))
                .Skip(request.PageSize * request.PageIndex).Take(request.PageSize)
                .Include(pi => pi.Store)
                .Include(p => p.OrderDetails)
                .ThenInclude(p => p.Product);
        }

        public async Task<long> Count(GetOrdersRequest request, bool includeDeleted = false)
        {
            return this.List(
                 p => (includeDeleted || !p.IsDeleted) &&
                (request.StoreId == null || p.StoreId == request.StoreId) &&
                (request.OrderNum == null || p.OrderNum == request.OrderNum) &&
                (request.OrderFrom == null || request.OrderTo == null ||
                (p.CreatedDate >= request.OrderFrom && p.CreatedDate <= request.OrderTo)) &&
                (request.PaymentFrom == null || request.PaymentTo == null ||
                (p.PaymenyDate >= request.PaymentFrom && p.PaymenyDate <= request.PaymentTo)) &&
                (request.Type == "issued" || request.FromUserId == null || p.FromUserId == request.FromUserId) &&
                (request.Type == "received" || request.ToUserId == null || p.ToUserId == request.ToUserId)).Count();
        }

        public async Task Publish(long Id)
        {
            var currentRecord = DbSet.FirstOrDefault(p => p.Id == Id);
            currentRecord.Published = true;
            this.DbSet.Update(currentRecord);
        }
        public async Task<long> GetOrderNum()
        {
            var Max = DbSet.Max(p => p.OrderNum);
            Max = Max == 0 ? 1 : Max + 1;
            return Max;
        }
    }
}
