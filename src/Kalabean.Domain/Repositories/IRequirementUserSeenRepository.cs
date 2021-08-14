using System.Linq;
using System.Threading.Tasks;
using Kalabean.Domain.Entities;

namespace Kalabean.Domain.Repositories
{
    public interface IRequirementUserSeenRepository : IRepository<RequirementUserSeen>
    {
        Task<RequirementUserSeen> GetById(long RequirementId,long UserId, bool includeDeleted = false);
    }
}
