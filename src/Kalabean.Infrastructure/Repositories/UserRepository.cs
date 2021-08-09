using Kalabean.Domain.Entities;
using Kalabean.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Kalabean.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly DbFactory _dbFactory;
        public UserRepository(DbFactory dbFactory) : base(dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<IQueryable<User>> Get()
        {
            return this.List(x => 1 == 1)
                .Include(u => u.RequirementUsers)
                .Include(u => u.Stores)
                .Include(u => u.RequirementAdmins)
                .Include(u => u.OrderHeaders);
        }

        public Task<User> GetById(long id)
        {
            return this.DbSet
                .Where(u => u.Id == id)
                .Include(u => u.RequirementUsers)
                .Include(u => u.Stores)
                .Include(u => u.RequirementAdmins)
                .Include(u => u.OrderHeaders)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}
