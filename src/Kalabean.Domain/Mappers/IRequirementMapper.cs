using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.Requirement;
using Kalabean.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Mappers
{
    public interface IRequirementMapper
    {
        Requirement Map(AddRequirementRequest request);
        Requirement Map(EditRequirementRequest request);
        RequirementResponse Map(Requirement request);
    }
}
