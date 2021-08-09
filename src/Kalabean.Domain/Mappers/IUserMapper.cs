using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.User;
using Kalabean.Domain.Responses;

namespace Kalabean.Domain.Mappers
{
    public interface IUserMapper
    {
        User Map(AddUserRequest request);
        UserResponse Map(User request);
        ThumbResponse<long> MapThumb(User request);
    }
}
