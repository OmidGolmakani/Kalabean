using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.TargetType;
using Kalabean.Domain.Responses;

namespace Kalabean.Domain.Mappers
{
    public class TargetTypeMapper : ITargetTypeMapper
    {
        public TargetTypeMapper()
        {

        }

        public TargetType Map(AddTargetTypeRequest request)
        {
            if (request == null) return null;

            var TargetType = new TargetType
            {
                Name = request.Name
            };

            return TargetType;
        }

        public TargetType Map(EditTargetTypeRequest request)
        {
            if (request == null) return null;

            var TargetType = new TargetType
            {
                Id = request.Id,
                Name = request.Name
            };

            return TargetType;
        }
        public TargetTypeResponse Map(TargetType TargetType)
        {
            if (TargetType == null) return null;

            var response = new TargetTypeResponse
            {
                Id = TargetType.Id,
                Name = TargetType.Name,
            };
            return response;
        }
    }
}
