using Kalabean.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Repositories
{
    public interface IProductImageRepository : IRepository<ProductImage>
    {
        Task<ProductImage> GetById(int id, bool includeDeleted = false);
        Task<IQueryable<ProductImage>> Get(bool includeDeleted = false);
    }
}
