using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Requests.ProductImage
{
    public class EditProductImageRequest
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public string Extention { get; set; }
    }
}
