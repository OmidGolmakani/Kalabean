using Kalabean.Domain.Requests.City;
using Kalabean.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kalabean.Domain.Services
{
    public interface IAccessRulesService
    {
        Task<IEnumerable<AccessRuleResponse>> GetRulesAsync();
    }
}
