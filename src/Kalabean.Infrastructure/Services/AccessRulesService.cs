using System.Linq;
using Kalabean.Domain.Mappers;
using Kalabean.Domain.Repositories;
using Kalabean.Domain.Requests.City;
using Kalabean.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Kalabean.Domain.Base;
using Kalabean.Domain.Services;
using Kalabean.Infrastructure.Files;

namespace Kalabean.Infrastructure.Services
{
    public class AccessRulesService : IAccessRulesService
    {
        private readonly IAccessRulesRepository _ruleRepository;
        public AccessRulesService(IAccessRulesRepository ruleRepository)
        {
            _ruleRepository = ruleRepository;
        }

        public async Task<IEnumerable<AccessRuleResponse>> GetRulesAsync()
        {
            var result = _ruleRepository.List(c => true);
            return result.Select(c => new AccessRuleResponse
            {
                Id = c.Id,
                Name = c.Name
            });
        }
    }
}
