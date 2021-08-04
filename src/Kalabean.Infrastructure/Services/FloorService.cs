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
    public class FloorService : IFloorService
    {
        private readonly IFloorRepository _repository;
        public FloorService(IFloorRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<FloorResponse>> GetFloorsAsync()
        {
            var result = await _repository.Get(false);
            return result.Select(c => new FloorResponse
            {
                Id = c.Id,
                Name = c.Name,
                Order = c.Order,
                ShoppingCenterId=c.ShoppingCenterId
            });
        }
    }
}
