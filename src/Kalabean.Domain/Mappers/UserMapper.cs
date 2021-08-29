using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.User;
using Kalabean.Domain.Responses;
using System.Linq;

namespace Kalabean.Domain.Mappers
{
    public class UserMapper : IUserMapper
    {
        private readonly IOrderHeaderMapper _order;
        private readonly IRequirementMapper _requirement;
        private readonly IStoreMapper _store;

        public UserMapper(IOrderHeaderMapper order,
                          IRequirementMapper requirement,
                          IStoreMapper store)
        {
            this._order = order;
            this._requirement = requirement;
            this._store = store;
        }

        public User Map(AddUserRequest request)
        {
            if (request == null) return null;

            var User = new User
            {
                Email = request.Email,
                Family = request.Family,
                Id = 0,
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                UserName = request.UserName
            };

            return User;
        }
        public User Map(EditUserRequest request)
        {
            if (request == null) return null;

            var User = new User
            {
                Email = request.Email,
                Family = request.Family,
                Id = request.Id,
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                UserName = request.UserName
            };

            return User;
        }

        public UserResponse Map(User user)
        {
            if (user == null) return null;

            return new UserResponse()
            {
                Email = user.Email,
                Family = user.Family,
                Id = user.Id,
                Name = user.Name,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                LockoutEnabled = user.LockoutEnabled,
                LockoutEnd = user.LockoutEnd != null ? Helper.PersionDate.GetShamsi((user.LockoutEnd ?? System.DateTimeOffset.Now)) : null,
                FromOrders = user.FromOrderHeaders != null ? user.FromOrderHeaders.Select(x => _order.Map(x)).ToList() : null,
                ToOrders = user.ToOrderHeaders != null ? user.ToOrderHeaders.Select(x => _order.Map(x)).ToList() : null,
                StoresThumb = user.Stores != null ? user.Stores.Select(x => _store.MapThumb(x)).ToList() : null,
                Requirements = user.RequirementUsers != null ? user.RequirementUsers.Select(x => _requirement.Map(x)).ToList() : null
            };
        }

        public ThumbResponse<long> MapThumb(User request)
        {
            if (request == null) return null;
            return new ThumbResponse<long>()
            {
                Id = request.Id,
                Name = string.Format("{0} {1}", request.Name, request.Family)
            };
        }
    }
}
