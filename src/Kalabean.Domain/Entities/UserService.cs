using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Entities
{
    public enum Subscriptiontype : byte
    {
        [Display(Name ="شخصی")]
        Personal = 1,
        [Display(Name = "تجاری")]
        Commercial = 2
    }
    public enum UserStatus : byte
    {
        [Display(Name = "تایید شده")]
        Accepted = 1,
        [Display(Name = "در انتظار تایید")]
        AwaitingApproval = 2,
        [Display(Name = "معلق")]
        Suspended = 3
    }
}
