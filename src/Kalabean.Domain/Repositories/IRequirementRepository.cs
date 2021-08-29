using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.Requirement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Repositories
{
    public interface IRequirementRepository : IRepository<Requirement>
    {
        Task<Requirement> GetById(long id, bool includeDeleted = false);
        Task<IQueryable<Requirement>> Get(GetRequirementsRequest request, bool includeDeleted = false);
        Task<int> Count(GetRequirementsRequest request, bool includeDeleted = false);
        Task ChangeStatus(long Id,RequirementStatus status);
    }
}
