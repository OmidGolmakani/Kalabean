using Kalabean.Domain.Entities;
using Kalabean.Domain.Repositories;
using Kalabean.Domain.Requests.Requirement;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kalabean.Infrastructure.Repositories
{
    public class RequirementRepository : Repository<Requirement>, IRequirementRepository
    {
        //private readonly DbFactory _dbFactory;
        public RequirementRepository(DbFactory dbFactory) : base(dbFactory) { }

        public Task<Requirement> GetById(long id, bool includeDeleted = false)
        {
            return this.DbSet
                .Where(r => r.Id == id && (includeDeleted || !r.IsDeleted))
                .Include(pi => pi.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<IQueryable<Requirement>> Get(GetRequirementsRequest request, bool includeDeleted = false)
        {
            return this
                .List(r => (includeDeleted || !r.IsDeleted) &&
                           (r.Conversations.Count() != 0 || r.Expire == 0 || r.CreatedDate.AddMinutes((double)r.Expire) > System.DateTime.Now) &&
                           (request.CategoryId == null || r.CategoryId == request.CategoryId) &&
                           (string.IsNullOrEmpty(request.ProductName) || r.ProductName.Contains(request.ProductName)) &&
                           (request.Status == RequirementStatus.All || r.RequirementStatus == (byte)request.Status) &&
                           (request.From == null || request.To == null || r.CreatedDate >= request.From && r.CreatedDate <= request.To) &&
                           ((request.ReqirementType == RequirementType.All && (request.UserId == null || (request.UserId != null && (r.UserId == request.UserId || r.Category.Stores.Any(s => s.UserId == request.UserId))))) ||
                           (request.ReqirementType == RequirementType.Sent && (request.UserId == null || (request.UserId != null && (r.UserId == request.UserId)))) ||
                           (request.ReqirementType == RequirementType.Received && (request.UserId == null || (request.UserId != null && (r.Category.Stores.Any(s => s.UserId == request.UserId)))))) &&
                           ((request.SeeReqirementType == SeeRequirementType.All && (request.UserId == null || (request.UserId != null && (r.RequirementUserSeen.UserId == request.UserId || r.RequirementUserSeen == null)))) ||
                           (request.SeeReqirementType == SeeRequirementType.Read && (request.UserId == null || (request.UserId != null && (r.RequirementUserSeen.UserId == request.UserId)))) ||
                           (request.SeeReqirementType == SeeRequirementType.UnRead && (request.UserId == null || (request.UserId != null && (r.RequirementUserSeen == null)))))
                           )
            .Skip(request.PageSize * request.PageIndex).Take(request.PageSize)
            .Include(pi => pi.Category)
            .ThenInclude(pi => pi.Stores)
            .ThenInclude(pi => pi.StoreUser);
        }
        public async Task<int> Count(GetRequirementsRequest request, bool includeDeleted = false)
        {
            return this
                .List(r => (includeDeleted || !r.IsDeleted) &&
                           (r.Conversations.Count() != 0 || r.Expire == 0 || r.CreatedDate.AddMinutes((double)r.Expire) > System.DateTime.Now) &&
                           (request.CategoryId == null || r.CategoryId == request.CategoryId) &&
                           (string.IsNullOrEmpty(request.ProductName) || r.ProductName.Contains(request.ProductName)) &&
                           (request.Status == RequirementStatus.All || r.RequirementStatus == (byte)request.Status) &&
                           (request.From == null || request.To == null || r.CreatedDate >= request.From && r.CreatedDate <= request.To) &&
                           ((request.ReqirementType == RequirementType.All && (request.UserId == null || (request.UserId != null && (r.UserId == request.UserId || r.Category.Stores.Any(s => s.UserId == request.UserId))))) ||
                           (request.ReqirementType == RequirementType.Sent && (request.UserId == null || (request.UserId != null && (r.UserId == request.UserId)))) ||
                           (request.ReqirementType == RequirementType.Received && (request.UserId == null || (request.UserId != null && (r.Category.Stores.Any(s => s.UserId == request.UserId)))))) &&
                           ((request.SeeReqirementType == SeeRequirementType.All && (request.UserId == null || (request.UserId != null && (r.RequirementUserSeen.UserId == request.UserId || r.RequirementUserSeen == null)))) ||
                           (request.SeeReqirementType == SeeRequirementType.Read && (request.UserId == null || (request.UserId != null && (r.RequirementUserSeen.UserId == request.UserId)))) ||
                           (request.SeeReqirementType == SeeRequirementType.UnRead && (request.UserId == null || (request.UserId != null && (r.RequirementUserSeen == null)))))
                           )
            .Skip(request.PageSize * request.PageIndex).Take(request.PageSize).Count();
        }

        public async Task ChangeStatus(long Id, int categoryId, RequirementStatus status)
        {
            var currentRecord = this.DbSet.Find(Id);
            if (currentRecord == null) return;
            currentRecord.RequirementStatus = (byte)status;
            currentRecord.CategoryId = categoryId;
            currentRecord.DateChangeStatus = System.DateTime.Now;
            currentRecord.AdminId = Helpers.JWTTokenManager.GetUserIdByToken();
            DbSet.Update(currentRecord);
        }

    }
}
