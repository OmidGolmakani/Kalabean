using Kalabean.Domain.Requests.PossibilitiesShopCenter;
using Kalabean.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kalabean.Domain.Services
{
    public interface IPossibilitiesShopCenterService
    {
        Task<ListPagingResponse<PossibilitiesShopCenterResponse>> GetPossibilitiesShopCentersAsync(GetPossibilitiesShopCentersRequest request);
        Task<PossibilitiesShopCenterResponse> GetPossibilitiesShopCenterAsync(GetPossibilitiesShopCenterRequest request);
        Task<PossibilitiesShopCenterResponse> AddPossibilitiesShopCenterAsync(AddPossibilitiesShopCenterRequest request);
        Task<PossibilitiesShopCenterResponse> EditPossibilitiesShopCenterAsync(EditPossibilitiesShopCenterRequest request);
        Task BatchDeletePossibilitiesShopCentersAsync(int[] ids);
        Task<int> Count(GetPossibilitiesShopCentersRequest request);
    }
}
