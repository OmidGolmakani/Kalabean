using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Kalabean.Domain.Requests.ResizeImage
{
    public class GetImageRequest
    {
        public IFormFile Image { get; set; }
        public Size ImageSize { get; set; }
    }
}
