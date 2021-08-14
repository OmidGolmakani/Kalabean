using Kalabean.Domain.Responses.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Responses
{
    public class RequirementUserSeenResponse 
    {
        public long RequirementId { get; set; }
        public long UserId { get; set; }
    }
}
