using System.Linq;
using System.Threading.Tasks;
using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.PossibilitiesShopCenter;

namespace Kalabean.Domain.Repositories
{
    public interface IPossibilitiesShopCenterRepository : IRepository<PossibilitiesShopCenter>
    {
        Task<PossibilitiesShopCenter> GetById(long id, bool includeDeleted = false);
        Task<IQueryable<PossibilitiesShopCenter>> Get(GetPossibilitiesShopCentersRequest request, bool includeDeleted = false);
        Task<int> Count(GetPossibilitiesShopCentersRequest request, bool includeDeleted = false);
    }
}
