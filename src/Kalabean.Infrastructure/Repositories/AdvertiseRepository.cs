using Kalabean.Domain.Entities;
using Kalabean.Domain.Repositories;
using Kalabean.Domain.Requests.Advertise;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Infrastructure.Repositories
{
    public class AdvertiseRepository : Repository<Advertise>, IAdvertiseRepository
    {
        private readonly DbFactory _dbFactory;
        public AdvertiseRepository(DbFactory dbFactory) : base(dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<long> Count(GetAdvertisingRequest request, bool includeDeleted = false)
        {
            var Count = this
                 .List(p => (includeDeleted || !p.IsDeleted) &&
                 (string.IsNullOrEmpty(request.Name) || (!string.IsNullOrEmpty(p.Name)
                 && p.Name.Contains(request.Name))) &&
                 (string.IsNullOrEmpty(request.Text) || (!string.IsNullOrEmpty(p.Text)
                 && p.Name.Contains(request.Text))) &&
                 (string.IsNullOrEmpty(request.Title) || (!string.IsNullOrEmpty(p.Title)
                 && p.Name.Contains(request.Title))))
                 .Skip(request.PageSize * request.PageIndex).Take(request.PageSize).Count();
            return Count;
        }

        public async Task<IQueryable<Advertise>> Get(GetAdvertisingRequest request, bool includeDeleted = false)
        {
            var q = this
                 .List(p => (includeDeleted || !p.IsDeleted) &&
                 (string.IsNullOrEmpty(request.Name) || (!string.IsNullOrEmpty(p.Name)
                 && p.Name.Contains(request.Name))) &&
                 (string.IsNullOrEmpty(request.Text) || (!string.IsNullOrEmpty(p.Text)
                 && p.Name.Contains(request.Text))) &&
                 (string.IsNullOrEmpty(request.Title) || (!string.IsNullOrEmpty(p.Title)
                 && p.Name.Contains(request.Title))))
                 .Skip(request.PageSize * request.PageIndex).Take(request.PageSize)
                 .Include(p=> p.Parent)
                 .Include(p => p.Child);
            return q;
        }

        public Task<Advertise> GetById(long id, bool includeDeleted = false)
        {
            return this.DbSet
             .Where(c => c.Id == id && (includeDeleted || !c.IsDeleted))
             .AsNoTracking()
             .FirstOrDefaultAsync();
        }
    }
}
