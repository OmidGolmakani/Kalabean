using Kalabean.Domain.Entities;
using Kalabean.Domain.Repositories;
using Kalabean.Domain.Requests.Article;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Infrastructure.Repositories
{
    public class ArticleRepository : Repository<Article>, IArticleRepository
    {
        private readonly DbFactory _dbFactory;
        public ArticleRepository(DbFactory dbFactory) : base(dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<int> Count(GetArticlesRequest request, bool includeDeleted = false)
        {
            var Count = this
                 .List(p => (includeDeleted || !p.IsDeleted) &&
                 (string.IsNullOrEmpty(request.Name) || ( !string.IsNullOrEmpty(p.Name)
                 && p.Name.Contains(request.Name)))).Count();
            return Count;
        }

        public async Task<IQueryable<Article>> Get(GetArticlesRequest request, bool includeDeleted = false)
        {
            var q = this
                 .List(p => (includeDeleted || !p.IsDeleted) &&
                 (string.IsNullOrEmpty(request.Name) || (!string.IsNullOrEmpty(p.Name)
                 && p.Name.Contains(request.Name))))
                 .Skip(request.PageSize * request.PageIndex).Take(request.PageSize)
                 .Include(a => a.AdminUser);
            return q;
        }

        public Task<Article> GetById(long id, bool includeDeleted = false)
        {
            return this.DbSet
             .Where(c => c.Id == id && (includeDeleted || !c.IsDeleted))
             .Include(c => c.AdminUser)
             .AsNoTracking()
             .FirstOrDefaultAsync();
        }
    }
}
