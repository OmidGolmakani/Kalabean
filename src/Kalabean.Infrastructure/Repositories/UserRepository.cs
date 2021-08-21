using Kalabean.Domain.Entities;
using Kalabean.Domain.Repositories;
using Kalabean.Domain.Requests.User;
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

        public async Task<IQueryable<User>> Get(GetUsersRequest request, bool includeDeleted = false)
        {
            return this.List(u => 
                                   string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(u.Name) || u.Name.Contains(request.Name) &&
                                   string.IsNullOrEmpty(request.PhoneNUmber) || string.IsNullOrEmpty(u.PhoneNumber) || u.Name.Contains(request.PhoneNUmber) &&
                                   string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(u.Email) || u.Name.Contains(request.Email))
                .Skip(request.PageSize * request.PageIndex).Take(request.PageSize)
                .Include(u => u.RequirementUsers)
                .Include(u => u.Stores)
                .Include(u => u.RequirementAdmins)
                .Include(u => u.OrderHeaders);
        }

        public Task<User> GetById(long id, bool includeDeleted = false)
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
        public async Task<long> Count(GetUsersRequest request, bool includeDeleted = false)
        {
            return this.List(u => 
                                   string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(u.Name) || u.Name.Contains(request.Name) &&
                                   string.IsNullOrEmpty(request.PhoneNUmber) || string.IsNullOrEmpty(u.PhoneNumber) || u.Name.Contains(request.PhoneNUmber) &&
                                   string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(u.Email) || u.Name.Contains(request.Email)).Count();
        }
    }
}
