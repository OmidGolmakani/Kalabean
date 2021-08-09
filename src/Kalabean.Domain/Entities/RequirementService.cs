using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Entities
{
    public enum RequirementStatus : byte
    {
        AwaitingApproval = 1,
        Accepted = 2,
        Rejected = 3
    }
    public enum TypePriceing
    {
        Desired = 1,
        Agreed = 2
    }
}
