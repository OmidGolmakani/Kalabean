using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Requests.Requirement
{
    public class SeenReqirementRequest
    {
        public long UserId { get; set; }
        public long ReqirementId { get; set; }
    }
}
