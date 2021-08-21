using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Requests.Advertise
{
    public class EditAdvertiseRequest
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public byte AdPositionId { get; set; }
        public string Name { get; set; }
        public string UrlLink { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public bool ImageEdited { get; set; }
        public IFormFile Image { get; set; }
    }
}
