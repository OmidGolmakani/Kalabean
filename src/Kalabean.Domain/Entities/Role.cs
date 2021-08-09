using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Kalabean.Domain.Entities
{
    public class Role : IdentityRole<long>
    {
        public Role()
        {
        }
    }
}
