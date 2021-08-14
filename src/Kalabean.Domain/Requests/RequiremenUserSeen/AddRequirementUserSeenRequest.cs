using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Requests.RequiremenUserSeen
{
    public class AddRequirementUserSeenRequest
    {
        public long RequiremenId { get; set; }
        public long UserId { get; set; }
    }
}
