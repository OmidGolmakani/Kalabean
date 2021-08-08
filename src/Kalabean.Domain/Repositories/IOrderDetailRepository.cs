using Kalabean.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Repositories
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        Task<OrderDetail> GetById (long id, bool includeDeleted=false);

    }
}
