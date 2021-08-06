using Kalabean.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetById(long id, bool includeDeleted = false);
        Task<IQueryable<Product>> Get(bool includeDeleted = false);
    }
}
