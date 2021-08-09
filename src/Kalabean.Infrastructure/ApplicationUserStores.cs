using Kalabean.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Kalabean.Domain.Entities
{
    public class ApplicationUserStore : UserStore<User,
                                                  Role,
                                                  AppDbContext, 
                                                  long, 
                                                  UserClaim,
                                                  UserRole, 
                                                  UserLogin, 
                                                  UserToken, 
                                                  RoleClaim>
    {
        public ApplicationUserStore(AppDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }
}
