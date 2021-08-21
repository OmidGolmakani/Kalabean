using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.PossibilitiesShopCenter;
using Kalabean.Domain.Responses;
using System;
using System.Linq;

namespace Kalabean.Domain.Mappers
{
    public class PossibilitiesShopCenterMapper : IPossibilitiesShopCenterMapper
    {
        private readonly IUserMapper user;

        public PossibilitiesShopCenterMapper(IUserMapper User)
        {
            user = User;
        }

        public PossibilitiesShopCenter Map(AddPossibilitiesShopCenterRequest request)
        {
            if (request == null) return null;

            var PossibilitiesShopCenter = new PossibilitiesShopCenter
            {
                Name = request.Name
            };

            PossibilitiesShopCenter.HasImage = request.Image != null && request.Image.Length > 0;
            return PossibilitiesShopCenter;
        }

        public PossibilitiesShopCenter Map(EditPossibilitiesShopCenterRequest request)
        {
            if (request == null) return null;

            var PossibilitiesShopCenter = new PossibilitiesShopCenter
            {
                Id = request.Id,
                Name = request.Name,
            };

            if (request.ImageEdited)
            {
                PossibilitiesShopCenter.HasImage = request.Image != null && request.Image.Length > 0;
            }
            return PossibilitiesShopCenter;
        }
        public PossibilitiesShopCenterResponse Map(PossibilitiesShopCenter PossibilitiesShopCenter)
        {
            if (PossibilitiesShopCenter == null) return null;

            var response = new PossibilitiesShopCenterResponse
            {
                Id = PossibilitiesShopCenter.Id,
                Name = PossibilitiesShopCenter.Name,
            };
            if (PossibilitiesShopCenter.HasImage)
                response.ImageUrl = $"/KL_ImagesRepo/PossibilitiesShopCenters/250_250/{PossibilitiesShopCenter.Id}.jpeg";
            return response;
        }

        public ThumbResponse<int> MapThumb(PossibilitiesShopCenter request)
        {
            if (request == null) return null;
            return new ThumbResponse<int>()
            {
                Id = request.Id,
                Name = request.Name
            };
        }

    }
}
