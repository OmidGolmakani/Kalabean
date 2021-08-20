using Kalabean.Domain.Entities;
using Kalabean.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kalabean.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        //private readonly DbFactory _dbFactory;
        public CategoryRepository(DbFactory dbFactory) : base(dbFactory) { }

        public Task<Category> GetById(int id, bool includeDeleted = false)
        {
            return this.DbSet
                .Where(c => c.Id == id && (includeDeleted || !c.IsDeleted))
                .Include(c => c.Parent)
                .Include(c => c.Children)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<IQueryable<Category>> Get(string name, int? parentId, bool includeDeleted = false)
        {
            return this
                .List(c => (includeDeleted || !c.IsDeleted) &&
                (string.IsNullOrEmpty(name) || (!string.IsNullOrEmpty(c.Name) && c.Name.Contains(name))) &&
                (!parentId.HasValue || (c.ParentId.HasValue && c.ParentId == parentId)))
                .Include(c => c.Parent)
                .Include(c => c.Children);
        }
    }
}
