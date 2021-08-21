using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.ProductComment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Repositories
{
    public interface IProductCommentRepository : IRepository<ProductComment>
    {
        Task<ProductComment> GetById(long id, bool includeDeleted = false);
        Task<IQueryable<ProductComment>> Get(GetProductCommentsRequest request,bool includeDeleted = false);
        Task<int> Count(GetProductCommentsRequest request, bool includeDeleted = false);
    }
}
