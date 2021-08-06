using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Responses
{
    public class ImageResizeResponse
    {
        public string FullPath { get; set; }
        public string FileName { get; set; }
        public Size Size { get; set; }
    }
}
