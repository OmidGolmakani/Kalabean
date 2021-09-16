using System.Linq;
using System.Threading.Tasks;
using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.Favorites;

namespace Kalabean.Domain.Repositories
{
    public interface IFavoriteRepository : IRepository<Favorites>
    {
        Task<Favorites> GetById(long id, bool includeDeleted = false);
        Task<IQueryable<Favorites>> Get(GetFavoritesRequest request, bool includeDeleted = false);
        Task<int> Count(GetFavoritesRequest request, bool includeDeleted = false);
    }
}
