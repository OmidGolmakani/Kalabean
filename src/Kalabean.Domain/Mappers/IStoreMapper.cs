using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.Store;
using Kalabean.Domain.Responses;

namespace Kalabean.Domain.Mappers
{
    public interface IStoreMapper
    {
        Store Map(AddStoreRequest request);
        Store Map(EditStoreRequest request);
        StoreResponse Map(Store request);
        ThumbResponse<int> MapThumb(Store request);
    }
}
