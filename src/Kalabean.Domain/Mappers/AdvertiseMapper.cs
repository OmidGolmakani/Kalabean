using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.Advertise;
using Kalabean.Domain.Responses;
using System;
using System.Linq;

namespace Kalabean.Domain.Mappers
{
    public class AdvertiseMapper : IAdvertiseMapper
    {
        private readonly IUserMapper user;

        public AdvertiseMapper(IUserMapper User)
        {
            user = User;
        }

        public Advertise Map(AddAdvertiseRequest request)
        {
            if (request == null) return null;

            var Advertise = new Advertise
            {
                AdPositionId = request.AdPositionId,
                Id = 0,
                Name = request.Name,
                Text = request.Text,
                Title = request.Title,
                UrlLink = request.UrlLink,
                ParentId = request.ParentId
            };

            Advertise.HasImage = request.Image != null && request.Image.Length > 0;
            return Advertise;
        }

        public Advertise Map(EditAdvertiseRequest request)
        {
            if (request == null) return null;

            var Advertise = new Advertise
            {
                AdPositionId = request.AdPositionId,
                Id = request.Id,
                Name = request.Name,
                Text = request.Text,
                Title = request.Title,
                UrlLink = request.UrlLink,
                ParentId = request.ParentId
            };

            if (request.ImageEdited)
            {
                Advertise.HasImage = request.Image != null && request.Image.Length > 0;
            }
            return Advertise;
        }
        public AdvertiseResponse Map(Advertise Advertise)
        {
            if (Advertise == null) return null;

            var response = new AdvertiseResponse
            {
                AdPositionId = Advertise.AdPositionId,
                Id = Advertise.Id,
                Name = Advertise.Name,
                Text = Advertise.Text,
                Title = Advertise.Title,
                UrlLink = Advertise.UrlLink,
                ParentId = Advertise.ParentId,
                ParentThumb = MapThumb(Advertise.Parent),
                ChildThumb = Advertise.Child != null ? Advertise.Child.Select(x => MapThumb(x)).ToList() : null
            };
            if (Advertise.HasImage)
                response.ImageUrl = $"/KL_ImagesRepo/Advertiseing/250_250/{Advertise.Id}.jpeg";
            return response;
        }

        public ThumbResponse<int> MapThumb(Advertise request)
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
