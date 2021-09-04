using Kalabean.Domain.Entities;
using Kalabean.Domain.Helper;
using Kalabean.Domain.Requests.ProductComment;
using Kalabean.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Mappers
{
    public class ProductCommentMapper : IProductCommentMapper
    {
        private readonly IProductMapper _product;
        private readonly IUserMapper _user;

        public ProductCommentMapper(IProductMapper product,
                                    IUserMapper user)
        {
            this._product = product;
            this._user = user;
        }

        public ProductComment Map(AddProductCommentRequest request)
        {
            if (request == null) return null;
            var response = new ProductComment()
            {
                CreatedDate = DateTime.Now,
                Description = request.Description,
                Email = request.Email,
                Family = request.Family,
                Id = 0,
                IsDeleted = false,
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                ProductId = request.ProductId,
                Status = (byte)ProductCommentService.AwaitingApproval
            };
            return response;
        }

        public ProductComment Map(EditProductCommentRequest request)
        {
            if (request == null) return null;
            var response = new ProductComment()
            {
                CreatedDate = DateTime.Now,
                Description = request.Description,
                Email = request.Email,
                Family = request.Family,
                Id = 0,
                IsDeleted = false,
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                ProductId = request.ProductId,
                Status = (byte)ProductCommentService.AwaitingApproval
            };
            return response;
        }

        public ProductCommentResponse Map(ProductComment request)
        {
            if (request == null) return null;
            var response = new ProductCommentResponse()
            {
                Description = request.Description,
                Email = request.Email,
                Family = request.Family,
                Id = request.Id,
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                ProductId = request.ProductId,
                Status = request.Status,
                AdminId = request.AdminId,
                DatePublished = request.DatePublished.ToDate(),
                AdminThumb = _user.MapThumb(request.AdminUser),
                UserThumb = _user.MapThumb(request.User),
                ProductThumb = _product.MapThumb(request.Product),

            };
            return response;
        }


    }
}
