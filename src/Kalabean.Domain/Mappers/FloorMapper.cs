using Kalabean.Domain.Entities;
using Kalabean.Domain.Responses;
using System.Collections.Generic;

namespace Kalabean.Domain.Mappers
{
    public class FloorMapper : IFloorMapper
    {
        public FloorResponse Map(Floor request)
        {
            if (request == null) return null;

            var floor = new FloorResponse
            {
                Id = request.Id,
                Name = request.Name,
                Order = request.Order,
                ShoppingCenterId = request.ShoppingCenterId
            };
            return floor;
        }

        public ThumbResponse<int> MapThumb(Floor request)
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
