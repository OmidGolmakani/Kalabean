using Kalabean.Domain.Entities;
using Kalabean.Domain.Helper;
using Kalabean.Domain.Requests.Favorites;
using Kalabean.Domain.Responses;
using System;

namespace Kalabean.Domain.Mappers
{
    public class FavoriteMapper : IFavoriteMapper
    {
        private readonly IUserMapper user;

        public FavoriteMapper(IUserMapper User)
        {
            user = User;
        }

        public Favorites Map(AddFavoriteRequest request)
        {
            if (request == null) return null;

            var Favorite = new Favorites
            {
                RelatedId = request.RelatedId,
                TypeId = request.TypeId
            };

            return Favorite;
        }
        public FavoriteResponse Map(Favorites Favorite)
        {
            if (Favorite == null) return null;

            var response = new FavoriteResponse
            {
                Id = Favorite.Id,
                RelatedId = Favorite.RelatedId,
                TypeId = Favorite.TypeId
            };
            return response;
        }
    }
}
