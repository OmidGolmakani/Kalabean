using System;
using System.Linq;
using System.Threading.Tasks;
using Kalabean.Domain.Entities;

namespace Kalabean.Domain.Repositories
{
    public interface IFloorRepository : IRepository<Floor>
    {
        Task<Floor> GetById(int id, bool includeDeleted = false);
        Task<IQueryable<Floor>> Get(bool includeDeleted = false);
    }
}
