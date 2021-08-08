using Kalabean.Domain.Entities;
using Kalabean.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kalabean.Infrastructure.Repositories
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        //private readonly DbFactory _dbFactory;
        public OrderDetailRepository(DbFactory dbFactory) : base(dbFactory) { }

        public Task<OrderDetail> GetById(long id, bool includeDeleted = false)
        {
            return this.DbSet
                .Where(p => p.Id == id && (includeDeleted || !p.IsDeleted))
                .Include(p => p.Product)
                .Include(p => p.OrderHeader)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}
