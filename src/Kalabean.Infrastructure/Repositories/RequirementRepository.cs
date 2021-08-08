using Kalabean.Domain.Entities;
using Kalabean.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kalabean.Infrastructure.Repositories
{
   public class RequirementRepository : Repository<Requirement>, IRequirementRepository
    {
        //private readonly DbFactory _dbFactory;
        public RequirementRepository(DbFactory dbFactory) : base(dbFactory) { }

        public Task<Requirement> GetById(long id, bool includeDeleted = false)
        {
            return this.DbSet
                .Where(pi => pi.Id == id && (includeDeleted || !pi.IsDeleted))
                .Include(pi => pi.Product)
                .Include(pi => pi.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<IQueryable<Requirement>> Get(bool includeDeleted = false)
        {
            return this
                .List(pi => includeDeleted || !pi.IsDeleted)
                .Include(pi => pi.Product)
                .Include(pi => pi.Category);
        }
    }
}
