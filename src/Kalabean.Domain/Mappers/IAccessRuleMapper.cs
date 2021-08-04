using Kalabean.Domain.Entities;
using Kalabean.Domain.Responses;

namespace Kalabean.Domain.Mappers
{
    public interface IAccessRuleMapper
    {
        AccessRuleResponse Map(AccessRule request);
    }
}
