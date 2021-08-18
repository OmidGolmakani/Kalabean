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
        private readonly ICategoryMapper _categoryMapper;
        private readonly IStoreMapper _storeMapper;
        private readonly IProductImageMapper _productImageMapper;

        public ProductMapper(ICategoryMapper categoryMapper,
                             IStoreMapper storeMapper,
                             IProductImageMapper productImageMapper)
        {
            this._categoryMapper = categoryMapper;
            this._storeMapper = storeMapper;
            this._productImageMapper = productImageMapper;
        }

        public Product Map(AddProductRequest request)
        {
            if (request == null) return null;
            Product product = new Product()
            {
                CategoryId = request.CategoryId,
                Manufacturer = request.Manufacturer,
                ArchivingDate = request.ArchivingDate,
                PublishingDate = request.PublishingDate,
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
                IsEnabled = request.IsEnabled,
                Series = request.Series,
                Barcode = request.Barcode,
                CompanyName = request.CompanyName,
                HtmlContent = request.HtmlContent,
                Keywords = request.Keywords,
            };

            if (request.Images != null)
            {
                product.ProductImages = new List<ProductImage>();
                foreach (var pi in request.Images)
                {
                    var _pi = new ProductImage()
                    {
                        Product = product,
                        IsDeleted = false,
                        Extention = System.IO.Path.GetExtension(pi.FileName)
                    };
                    product.ProductImages.Add(_pi);
                }
            }
            return product;
        }

        public Product Map(EditProductRequest request)
        {
            if (request == null) return null;
            var response = new Product()
            {
                CategoryId = request.CategoryId,
                Manufacturer = request.Creator,
                ArchivingDate = request.DateArchive,
                PublishingDate = request.DatePublish,
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
                IsEnabled = request.Publish,
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
                Id= request.Id,
                Creator = request.Manufacturer,
                DateArchive = request.ArchivingDate,
                DatePublish = request.PublishingDate,
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
                Publish = request.IsEnabled,
                Series = request.Series,
                CategoryThumb = _categoryMapper.MapThumb(request.Category),
                StoreThumb = _storeMapper.MapThumb(request.Store)
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
