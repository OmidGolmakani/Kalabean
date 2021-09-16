using Kalabean.Domain.Entities;
using Kalabean.Domain.Repositories;
using Kalabean.Domain.Requests.Favorites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Infrastructure.Repositories
{
    public class FavoriteRepository : Repository<Favorites>, IFavoriteRepository
    {
        private readonly DbFactory _dbFactory;
        public FavoriteRepository(DbFactory dbFactory) : base(dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<int> Count(GetFavoritesRequest request, bool includeDeleted = false)
        {
            var Count = this
                 .List(p => (includeDeleted || !p.IsDeleted) &&
                 (request.TypeId == null || p.TypeId == request.TypeId) ||
                 (request.UserId == null || p.UserId == request.UserId))
                 .Skip(request.PageSize * request.PageIndex).Take(request.PageSize).Count();
            return Count;
        }

        public async Task<IQueryable<Favorites>> Get(GetFavoritesRequest request, bool includeDeleted = false)
        {
            var q = this
                 .List(p => (includeDeleted || !p.IsDeleted) &&
                 (request.TypeId == null || p.TypeId == request.TypeId) ||
                 (request.UserId == null || p.UserId == request.UserId))
                 .Skip(request.PageSize * request.PageIndex).Take(request.PageSize)
                 .Include(f => f.User);
            return q;
        }

        public Task<Favorites> GetById(long id, bool includeDeleted = false)
        {
            return this.DbSet
             .Where(c => c.Id == id && (includeDeleted || !c.IsDeleted))
             .Include(c => c.User)
             .AsNoTracking()
             .FirstOrDefaultAsync();
        }
    }
}
