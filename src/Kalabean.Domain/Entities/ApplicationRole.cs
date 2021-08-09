using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Kalabean.Domain.Entities
{
    public class ApplicationRole : IdentityRole<long>
    {
        public ApplicationRole()
        {
        }
    }
}
