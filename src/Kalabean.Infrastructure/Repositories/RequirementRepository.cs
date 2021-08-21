using Kalabean.Domain.Entities;
using Kalabean.Domain.Repositories;
using Kalabean.Domain.Requests.Requirement;
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
                .Where(r => r.Id == id && (includeDeleted || !r.IsDeleted))
                .Include(pi => pi.Product)
                .Include(pi => pi.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<IQueryable<Requirement>> Get(GetRequirementsRequest request, bool includeDeleted = false)
        {
            return this
                .List(r => (includeDeleted || !r.IsDeleted) &&
                            (request.CategoryId == null || r.CategoryId == request.CategoryId) &&
                            (request.UserId == null || r.UserId == request.UserId) &&
                            (string.IsNullOrEmpty(request.ProductName) ||
                             r.Product.ProductName.Contains(request.ProductName)))
                .Include(pi => pi.Product)
                .Include(pi => pi.Category);
        }
        public async Task<int> Count(GetRequirementsRequest request, bool includeDeleted = false)
        {
            return this
                .List(r => (includeDeleted || !r.IsDeleted) &&
                            (request.CategoryId == null || r.CategoryId == request.CategoryId) &&
                            (request.UserId == null || r.UserId == request.UserId) &&
                            (string.IsNullOrEmpty(request.ProductName) ||
                             r.Product.ProductName.Contains(request.ProductName))).Count();
        }
    }
}
