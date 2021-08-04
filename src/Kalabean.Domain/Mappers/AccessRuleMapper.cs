using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.Category;
using Kalabean.Domain.Responses;
using System.Collections.Generic;

namespace Kalabean.Domain.Mappers
{
    public class AccessRuleMapper : IAccessRuleMapper
    {
        public AccessRuleResponse Map(AccessRule request)
        {
            if (request == null) return null;

            var accessRule = new AccessRuleResponse
            {
                Id = request.Id,
                Name = request.Name
            };
            return accessRule;
        }
    }
}
