using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Entities
{
    public enum RequirementStatus : byte
    {
        All = 0,
        AwaitingApproval = 1,
        Accepted = 2,
        Rejected = 3
    }
    public enum RequirementTypePriceing : byte
    {
        Desired = 1,
        Agreed = 2
    }
    public enum RequirementType : byte
    {
        All = 0,
        Received = 1,
        Sent = 2
    }
    public enum SeeRequirementType : byte
    {
        All = 0,
        Read = 1,
        UnRead = 2
    }
}
