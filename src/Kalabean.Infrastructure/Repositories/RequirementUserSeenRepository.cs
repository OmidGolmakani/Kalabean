using Kalabean.Domain.Entities;
using Kalabean.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kalabean.Infrastructure.Repositories
{
    public class RequirementUserSeenRepository : Repository<RequirementUserSeen>, IRequirementUserSeenRepository
    {
        private readonly UserManager<User> _userManager;

        //private readonly DbFactory _dbFactory;
        public RequirementUserSeenRepository(DbFactory dbFactory,
                                             UserManager<User> userManager) : base(dbFactory)
        {
            this._userManager = userManager;
        }
        public override RequirementUserSeen Add(RequirementUserSeen entity)
        {
            var Get = GetById(entity.RequirementId, entity.UserId);
            Get.Wait();
            if (Get.Result == null)
            {
                return base.Add(entity);
            }
            else
            {
                return null;
            }
        }

        public Task<RequirementUserSeen> GetById(long RequirementId, long UserId, bool includeDeleted = false)
        {
            if (_userManager.Users.FirstOrDefault(x => x.Id == UserId) == null ||
                _userManager.Users.FirstOrDefault(x => x.LockoutEnabled == true && x.LockoutEnd != null && x.LockoutEnd > DateTimeOffset.Now) != null)
            {
                throw new Exception("User is Lockout or Notfound");
            }
            return this.DbSet
               .Where(pi => pi.UserId == UserId && (includeDeleted || !pi.IsDeleted))
               .Include(pi => pi.User)
               .Include(pi => pi.Requirement)
               .AsNoTracking()
               .FirstOrDefaultAsync();
        }
    }
}
