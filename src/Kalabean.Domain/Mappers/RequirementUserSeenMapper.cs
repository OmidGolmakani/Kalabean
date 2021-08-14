using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.RequiremenUserSeen;
using Kalabean.Domain.Responses;
using System.Collections.Generic;

namespace Kalabean.Domain.Mappers
{
    public class RequirementUserSeenMapper : IRequirementUserSeenMapper
    {
        public RequirementUserSeenMapper()
        {
        }

        public RequirementUserSeen Map(AddRequirementUserSeenRequest request)
        {
            if (request == null) return null;

            var RequirementUserSeen = new RequirementUserSeen
            {
                UserId = request.UserId,
                RequirementId = request.RequiremenId
            };
            return RequirementUserSeen;
        }

        public RequirementUserSeenResponse Map(RequirementUserSeen RequirementUserSeen)
        {
            if (RequirementUserSeen == null) return null;

            var response = new RequirementUserSeenResponse
            {
                UserId = RequirementUserSeen.UserId,
                RequirementId = RequirementUserSeen.RequirementId

            };
            return response;
        }
    }
}
