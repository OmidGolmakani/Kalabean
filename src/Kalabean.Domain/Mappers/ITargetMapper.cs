using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.TargetType;
using Kalabean.Domain.Responses;

namespace Kalabean.Domain.Mappers
{
    public interface ITargetTypeMapper
    {
        TargetType Map(AddTargetTypeRequest request);
        TargetType Map(EditTargetTypeRequest request);
        TargetTypeResponse Map(TargetType request);

    }
}
