using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.ProductComment;
using Kalabean.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Mappers
{
    public interface IProductCommentMapper
    {
        ProductComment Map(AddProductCommentRequest request);
        ProductComment Map(EditProductCommentRequest request);
        ProductCommentResponse Map(ProductComment request);
    }
}
