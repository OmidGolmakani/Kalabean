using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.PossibilitiesShopCenter;
using Kalabean.Domain.Responses;

namespace Kalabean.Domain.Mappers
{
    public interface IPossibilitiesShopCenterMapper
    {
        PossibilitiesShopCenter Map(AddPossibilitiesShopCenterRequest request);
        PossibilitiesShopCenter Map(EditPossibilitiesShopCenterRequest request);
        PossibilitiesShopCenterResponse Map(PossibilitiesShopCenter request);
        ThumbResponse<int> MapThumb(PossibilitiesShopCenter request);
    }
}
