using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.Product;
using Kalabean.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Mappers
{
    public class ProductMapper : IProductMapper
    {
        private readonly ICategoryMapper _category;
        private readonly IStoreMapper _store;
        private readonly IProductImageMapper _productImage;

        public ProductMapper(ICategoryMapper category,
                             IStoreMapper store)
        {
            this._category = category;
            this._store = store;
        }

        public Product Map(AddProductRequest request)
        {
            if (request == null) return null;
            var response = new Product()
            {
                CategoryId = request.CategoryId,
                Creator = request.Creator,
                DateArchive = request.DateArchive,
                DatePublish = request.DatePublish,
                Discount = request.Discount,
                IsDeleted = false,
                Num = request.Num,
                Order = request.Order,
                Price = request.Price,
                ProductName = request.ProductName,
                StoreId = request.StoreId,
                LinkProduct = request.LinkProduct,
                IsNew = request.IsNew,
                Description = request.Description,
                Model = request.Model,
                Properties = request.Properties,
                Publish = request.Publish,
                Series = request.Series,
                Id = 0
            };
            foreach (var pi in request.Images)
            {
                var _pi = new ProductImage()
                {
                    Product = response,
                    Id = 0,
                    IsDeleted = false,
                    ProductId = 0,
                    Extention = System.IO.Path.GetExtension(pi.FileName)
                };
                response.ProductImages.Add(_pi);
            }
            return response;
        }

        public Product Map(EditProductRequest request)
        {
            if (request == null) return null;
            var response = new Product()
            {
                CategoryId = request.CategoryId,
                Creator = request.Creator,
                DateArchive = request.DateArchive,
                DatePublish = request.DatePublish,
                Discount = request.Discount,
                IsDeleted = false,
                Num = request.Num,
                Order = request.Order,
                Price = request.Price,
                ProductName = request.ProductName,
                StoreId = request.StoreId,
                LinkProduct = request.LinkProduct,
                IsNew = request.IsNew,
                Description = request.Description,
                Model = request.Model,
                Properties = request.Properties,
                Publish = request.Publish,
                Series = request.Series,
                Id = request.Id,
            };
            return response;
        }

        public ProductResponse Map(Product request)
        {
            if (request == null) return null;
            var response = new ProductResponse()
            {
                CategoryId = request.CategoryId,
                Creator = request.Creator,
                DateArchive = request.DateArchive,
                DatePublish = request.DatePublish,
                Discount = request.Discount,
                Num = request.Num,
                Order = request.Order,
                Price = request.Price,
                ProductName = request.ProductName,
                StoreId = request.StoreId,
                Description = request.Description,
                IsNew = request.IsNew,
                LinkProduct = request.LinkProduct,
                Model = request.Model,
                Properties = request.Properties,
                Publish = request.Publish,
                Series = request.Series,
                CategoryThumb = _category.MapThumb(request.Category),
                StoreThumb = _store.MapThumb(request.Store)
            };
            return response;
        }

        public ThumbResponse<long> MapThumb(Product request)
        {
            var response = new ThumbResponse<long>()
            {
                Id = request.Id,
                Name = request.ProductName
            };
            return response;
        }
    }
}
