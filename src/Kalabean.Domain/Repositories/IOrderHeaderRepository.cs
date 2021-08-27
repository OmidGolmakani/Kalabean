using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.OrderHeader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Repositories
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        Task<OrderHeader> GetById(long id, bool includeDeleted = false);
        Task<IQueryable<OrderHeader>> Get(GetOrdersRequest request, bool includeDeleted = false);
        Task<long> Count(GetOrdersRequest request, bool includeDeleted = false);
        Task Publish(long Id);
        Task<long> GetOrderNum();
    }
}
