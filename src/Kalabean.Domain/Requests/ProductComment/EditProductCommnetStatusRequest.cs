using Kalabean.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Requests.ProductComment
{
    public class EditProductCommnetStatusRequest
    {
        public long Id { get; set; }
        public ProductCommentService Status { get; set; }
    }
}
