using Kalabean.Domain.Requests.ProductComment;
using Kalabean.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Services
{
    public interface IProductCommentService
    {
        Task<ListPageingResponse<ProductCommentResponse>> GetProductCommentsAsync(GetProductCommentsRequest request);
        Task<ProductCommentResponse> GetProductCommentAsync(GetProductCommentRequest request);
        Task<ProductCommentResponse> AddProductCommentAsync(AddProductCommentRequest request);
        Task<ProductCommentResponse> EditProductCommentAsync(EditProductCommentRequest request);
        Task BatchDeleteProductCommentsAsync(long[] ids);
        Task UpdateProductCommentStatusAsync(EditProductCommnetStatusRequest request);
    }
}
