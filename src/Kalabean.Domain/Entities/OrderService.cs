using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Entities
{
    public enum OrderStatus : byte
    {
        AwaitingApproval = 1,
        Accepted = 2,
        Rejected = 3
    }
    public enum OrderType : byte
    {
        All = 0,
        Issued = 1,
        Received = 2
    }
}
