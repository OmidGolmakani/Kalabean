using Kalabean.Domain.Requests.Favorites;
using Kalabean.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kalabean.Domain.Services
{
    public interface IFavoriteService
    {
        Task<ListPagingResponse<FavoriteResponse>> GetFavoritesAsync(GetFavoritesRequest request);
        Task<FavoriteResponse> GetFavoriteAsync(GetFavoriteRequest request);
        Task<FavoriteResponse> AddFavoriteAsync(AddFavoriteRequest request);
        Task BatchDeleteFavoritesAsync(long[] ids);
        Task<long> Count(GetFavoritesRequest request);
    }
}
