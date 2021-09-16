using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.Favorites;
using Kalabean.Domain.Responses;

namespace Kalabean.Domain.Mappers
{
    public interface IFavoriteMapper
    {
        Favorites Map(AddFavoriteRequest request);
        FavoriteResponse Map(Favorites request);
    }
}
