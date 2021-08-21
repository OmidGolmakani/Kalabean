using Kalabean.Domain.Entities;
using Kalabean.Domain.Repositories;
using Kalabean.Domain.Requests.PossibilitiesShopCenter;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Infrastructure.Repositories
{
    public class PossibilitiesShopCenterRepository : Repository<PossibilitiesShopCenter>, IPossibilitiesShopCenterRepository
    {
        private readonly DbFactory _dbFactory;
        public PossibilitiesShopCenterRepository(DbFactory dbFactory) : base(dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<int> Count(GetPossibilitiesShopCentersRequest request, bool includeDeleted = false)
        {
            var Count = this
                 .List(p => (includeDeleted || !p.IsDeleted) &&
                 (string.IsNullOrEmpty(request.Name) || (!string.IsNullOrEmpty(p.Name)
                 && p.Name.Contains(request.Name))))
                 .Skip(request.PageSize * request.PageIndex).Take(request.PageSize).Count();
            return Count;
        }

        public async Task<IQueryable<PossibilitiesShopCenter>> Get(GetPossibilitiesShopCentersRequest request, bool includeDeleted = false)
        {
            var q = this
                 .List(p => (includeDeleted || !p.IsDeleted) &&
                 (string.IsNullOrEmpty(request.Name) || (!string.IsNullOrEmpty(p.Name)
                 && p.Name.Contains(request.Name))));
            return q;
        }

        public Task<PossibilitiesShopCenter> GetById(long id, bool includeDeleted = false)
        {
            return this.DbSet
             .Where(c => c.Id == id && (includeDeleted || !c.IsDeleted))
             .AsNoTracking()
             .FirstOrDefaultAsync();
        }
    }
}
