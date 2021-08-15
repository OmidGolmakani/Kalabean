using Kalabean.Domain.Entities;
using Kalabean.Domain.Repositories;
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

        public async Task<IQueryable<Article>> Get(bool includeDeleted = false)
        {
            return this
                 .List(p => includeDeleted || !p.IsDeleted)
                 .Include(a => a.AdminUser);
        }

        public Task<Article> GetById(long id, bool includeDeleted = false)
        {
            return this.DbSet
             .Where(c => c.Id == id && (includeDeleted || !c.IsDeleted))
             .Include(c=> c.AdminUser)
             .AsNoTracking()
             .FirstOrDefaultAsync();
        }
    }
}
