using Kalabean.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Requests.Requirement
{
    public class PublishRequirementRequest
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public RequirementStatus Status { get; set; }
    }
}
