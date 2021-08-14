using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.RequiremenUserSeen;
using Kalabean.Domain.Responses;

namespace Kalabean.Domain.Mappers
{
    public interface IRequirementUserSeenMapper
    {
        RequirementUserSeen Map(AddRequirementUserSeenRequest request);
        RequirementUserSeenResponse Map(RequirementUserSeen request);

    }
}
