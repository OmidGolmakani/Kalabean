using Kalabean.Domain.Entities;
using Kalabean.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Kalabean.Infrastructure.Repositories
{
    public class RolePermissionRepository : Repository<RolePermission>, IRolePermissionRepository
    {
        private readonly DbFactory _dbFactory;
        public RolePermissionRepository(DbFactory dbFactory) : base(dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<IQueryable<RolePermission>> Get(long RoleId)
        {
            return this.List(x => x.RoleId == RoleId).Include(r => r.Role);
        }

        public Task<RolePermission> GetById(long id)
        {
            return this.DbSet
                .Where(u => u.Id == id)
                .Include(r => r.Role)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}
