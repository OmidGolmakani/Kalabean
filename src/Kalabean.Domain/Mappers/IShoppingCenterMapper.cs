using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.ShoppingCenter;
using Kalabean.Domain.Responses;

namespace Kalabean.Domain.Mappers
{
    public interface IShoppingCenterMapper
    {
        ShoppingCenter Map(AddShoppingCenterRequest request);
        ShoppingCenter Map(EditShoppingCenterRequest request);
        ShoppingCenterResponse Map(ShoppingCenter request);
    }
}
