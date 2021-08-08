using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain
{
    public enum OrderStatus : byte
    {
        AwaitingApproval = 1,
        Accepted = 2,
        Rejected = 3
    }
}
