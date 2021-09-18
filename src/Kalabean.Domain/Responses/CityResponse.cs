using System;
using System.Collections.Generic;
using System.Text;

namespace Kalabean.Domain.Responses
{
    public class CityResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte? Order { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public byte? State { get; set; }
        public ThumbResponse<int> ParentThumb { get; set; }
    }
}
