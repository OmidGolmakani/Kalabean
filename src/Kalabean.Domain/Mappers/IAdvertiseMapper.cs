using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.Advertise;
using Kalabean.Domain.Responses;

namespace Kalabean.Domain.Mappers
{
    public interface IAdvertiseMapper
    {
        Advertise Map(AddAdvertiseRequest request);
        Advertise Map(EditAdvertiseRequest request);
        AdvertiseResponse Map(Advertise request);
        ThumbResponse<int> MapThumb(Advertise request);
    }
}
