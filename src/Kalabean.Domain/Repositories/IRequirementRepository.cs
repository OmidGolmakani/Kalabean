using Kalabean.Domain.Entities;
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
        Task<IQueryable<Requirement>> Get(bool includeDeleted = false);
    }
}
