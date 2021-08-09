using Kalabean.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Kalabean.Domain.Entities
{
    public class ApplicationUserStore : UserStore<ApplicationUser,
                                                  ApplicationRole,
                                                  AppDbContext, 
                                                  long, 
                                                  ApplicationUserClaim,
                                                  ApplicationUserRole, 
                                                  ApplicationUserLogin, 
                                                  ApplicationUserToken, 
                                                  ApplicationRoleClaim>
    {
        public ApplicationUserStore(AppDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }
}
