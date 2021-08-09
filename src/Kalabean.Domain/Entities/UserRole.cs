using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kalabean.Domain.Entities
{
    public class UserRole : IdentityUserRole<long>
    {
    }
}
