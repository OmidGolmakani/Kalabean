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
                (p.PaymentDate >= request.PaymentFrom && p.PaymentDate <= request.PaymentTo)) &&
                ((request.OrderType == Domain.OrderType.All && (request.UserId == null || p.FromUserId == request.UserId || p.ToUserId == request.UserId)) ||
                (request.OrderType == Domain.OrderType.Issued && (request.UserId == null || p.FromUserId == request.UserId)) ||
                (request.OrderType == Domain.OrderType.Received && (request.UserId == null || p.ToUserId == request.UserId))))
                .Skip(request.PageSize * request.PageIndex).Take(request.PageSize)
                .Include(pi => pi.Store)
                .Include(p => p.OrderDetails)
                .ThenInclude(p => p.Product);
        }

        public async Task<int> Count(GetOrdersRequest request, bool includeDeleted = false)
        {
            return this
                .List(
                p => (includeDeleted || !p.IsDeleted) &&
                (request.StoreId == null || p.StoreId == request.StoreId) &&
                (request.OrderNum == null || p.OrderNum == request.OrderNum) &&
                (request.OrderFrom == null || request.OrderTo == null ||
                (p.CreatedDate >= request.OrderFrom && p.CreatedDate <= request.OrderTo)) &&
                (request.PaymentFrom == null || request.PaymentTo == null ||
                (p.PaymentDate >= request.PaymentFrom && p.PaymentDate <= request.PaymentTo)) &&
                ((request.OrderType == Domain.OrderType.All && (request.UserId == null || p.FromUserId == request.UserId || p.ToUserId == request.UserId)) ||
                (request.OrderType == Domain.OrderType.Issued && (request.UserId == null ||  p.FromUserId == request.UserId)) ||
                (request.OrderType == Domain.OrderType.Received && (request.UserId == null || p.ToUserId == request.UserId))))
                .Skip(request.PageSize * request.PageIndex).Take(request.PageSize).Count();
        }

        public async Task Publish(long Id)
        {
            var currentRecord = DbSet.FirstOrDefault(p => p.Id == Id);
            currentRecord.Published = true;
            this.DbSet.Update(currentRecord);
        }
        public async Task<long> GetOrderNum()
        {
            long max = 0;
            if (DbSet.Count() > 0)
                max = DbSet.Max(p => p.OrderNum);

            return max + 1;
        }
    }
}
