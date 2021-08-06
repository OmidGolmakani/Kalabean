using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.ShoppingCenter;
using Kalabean.Domain.Responses;

namespace Kalabean.Domain.Mappers
{
    public interface IShoppingCenterTypeMapper
    {
        ShoppingCenterType Map(AddShoppingCenterTypeRequest request);
        ShoppingCenterType Map(EditShoppingCenterTypeRequest request);
        ShoppingCenterTypeResponse Map(ShoppingCenterType request);
        ThumbResponse<int> MapThump(ShoppingCenterType request);

    }
}
