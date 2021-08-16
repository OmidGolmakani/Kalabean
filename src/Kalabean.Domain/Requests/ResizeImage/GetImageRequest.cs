using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Kalabean.Domain.Requests.ResizeImage
{
    public class GetImageRequest<T>
    {
        public T Id { get; set; }
        public string ImageUrl { get; set; }
        public string Folder { get; set; }
        public Size ImageSize { get; set; }
    }
}
