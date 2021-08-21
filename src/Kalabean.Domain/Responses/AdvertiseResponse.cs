using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Responses
{
    public class AdvertiseResponse
    {
        public int Id { get; set; }
        public byte AdPositionId { get; set; }
        public string UrlLink { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string ImageUrl { get; set; }
    }
}
