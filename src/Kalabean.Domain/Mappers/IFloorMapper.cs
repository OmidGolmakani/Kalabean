using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.Category;
using Kalabean.Domain.Responses;

namespace Kalabean.Domain.Mappers
{
    public interface IFloorMapper
    {
        FloorResponse Map(Floor request);
        ThumbResponse<int> MapThumb(Floor request);
    }
}
