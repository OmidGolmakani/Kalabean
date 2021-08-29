using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Requests.Requirement
{
    public class GetRequirementsRequest : Page.PageRequest
    {
        public int? CategoryId { get; set; }
        public string ProductName { get; set; }
        public long? UserId { get; set; }
        public Entities.RequirementType ReqirementType { get; set; }
        public Entities.SeeRequirementType SeeReqirementType { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public Entities.RequirementStatus Status { get; set; }
    }
}
