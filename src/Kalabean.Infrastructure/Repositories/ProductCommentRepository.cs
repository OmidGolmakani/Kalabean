using Kalabean.Domain.Entities;
using Kalabean.Domain.Repositories;
using Kalabean.Domain.Requests.ProductComment;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Infrastructure.Repositories
{
    public class ProductCommentRepository : Repository<ProductComment>, IProductCommentRepository
    {
        public ProductCommentRepository(DbFactory dbFactory) : base(dbFactory) { }
        public async Task<long> Count(GetProductCommentsRequest request, bool includeDeleted = false)
        {
            return this.List(p => (includeDeleted || !p.IsDeleted) &&
                           (request.ProductId == null || p.ProductId == request.ProductId) &&
                           (request.UserId == null || p.UserId == request.UserId) &&
                           (string.IsNullOrEmpty(request.Name) ||
                            string.IsNullOrEmpty(request.Name) || p.Name.Contains(request.Name)) &&
                            (string.IsNullOrEmpty(request.Family) ||
                            string.IsNullOrEmpty(request.Family) || p.Name.Contains(request.Family)) &&
                            (string.IsNullOrEmpty(request.Email) ||
                            string.IsNullOrEmpty(request.Email) || p.Name.Contains(request.Email)) &&
                            (string.IsNullOrEmpty(request.PhoneNumber) ||
                            string.IsNullOrEmpty(request.PhoneNumber) || p.Name.Contains(request.PhoneNumber))
                           ).Count();
        }

        public async Task<IQueryable<ProductComment>> Get(GetProductCommentsRequest request, bool includeDeleted = false)
        {
            return this.List(p => (includeDeleted || !p.IsDeleted) &&
                           (request.ProductId == null || p.ProductId == request.ProductId) &&
                           (request.UserId == null || p.UserId == request.UserId) &&
                           (string.IsNullOrEmpty(request.Name) ||
                            string.IsNullOrEmpty(request.Name) || p.Name.Contains(request.Name)) &&
                            (string.IsNullOrEmpty(request.Family) ||
                            string.IsNullOrEmpty(request.Family) || p.Name.Contains(request.Family)) &&
                            (string.IsNullOrEmpty(request.Email) ||
                            string.IsNullOrEmpty(request.Email) || p.Name.Contains(request.Email)) &&
                            (string.IsNullOrEmpty(request.PhoneNumber) ||
                            string.IsNullOrEmpty(request.PhoneNumber) || p.Name.Contains(request.PhoneNumber))
                            ).Skip(request.PageSize * request.PageIndex).Take(request.PageSize)
                            .Include(p => p.Product)
                            .Include(p => p.User)
                            .Include(p => p.AdminUser);

        }

        public Task<ProductComment> GetById(long id, bool includeDeleted = false)
        {
            return this.DbSet
               .Where(p => p.Id == id && (includeDeleted || !p.IsDeleted))
               .Include(p => p.Product)
               .Include(p => p.User)
               .Include(p => p.AdminUser)
               .AsNoTracking()
               .FirstOrDefaultAsync();
        }

    }
}
