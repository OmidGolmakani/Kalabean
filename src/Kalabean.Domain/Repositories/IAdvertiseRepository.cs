using System.Linq;
using System.Threading.Tasks;
using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.Advertise;

namespace Kalabean.Domain.Repositories
{
    public interface IAdvertiseRepository : IRepository<Advertise>
    {
        Task<Advertise> GetById(long id, bool includeDeleted = false);
        Task<IQueryable<Advertise>> Get(GetAdvertisingRequest request, bool includeDeleted = false);
        Task<int> Count(GetAdvertisingRequest request, bool includeDeleted = false);
    }
}
