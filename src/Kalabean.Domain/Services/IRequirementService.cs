﻿using Kalabean.Domain.Requests.Requirement;
using Kalabean.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kalabean.Domain.Services
{
    public interface IRequirementService
    {
        Task<ListPageingResponse<RequirementResponse>> GetRequirementsAsync(GetRequirementsRequest request);
        Task<RequirementResponse> GetRequirementAsync(GetRequirementRequest request);
        Task<RequirementResponse> AddRequirementAsync(AddRequirementRequest request);
        Task<RequirementResponse> EditRequirementAsync(EditRequirementRequest request);
        Task BatchDeleteRequirementsAsync(long[] ids);
    }
}
