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
                Keywords = request.Keywords
            };

            product.HasFile = request.File != null && request.File.Length > 0;
            product.FileExtention = product.HasFile ?
                System.IO.Path.GetExtension(request.File.FileName) :
                null;

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
            Product product = new Product()
            {
                Id = request.Id,
                CategoryId = request.CategoryId,
                Manufacturer = request.Manufacturer,
                ArchivingDate = request.ArchivingDate,
                PublishingDate = request.PublishingDate,
                Discount = request.Discount,
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
                Keywords = request.Keywords
            };
            if (request.FileEdited)
            {
                product.HasFile = request.File != null && request.File.Length > 0;
                product.FileExtention = product.HasFile ?
                    System.IO.Path.GetExtension(request.File.FileName) :
                    null;
            }

            return product;
        }

        public ProductResponse Map(Product product)
        {
            if (product == null) return null;
            var response = new ProductResponse()
            {

                Id = product.Id,
                Manufacturer = product.Manufacturer,
                ArchivingDate = product.ArchivingDate?.ToString("yyyy/MM/dd"),
                PublishingDate = product.ArchivingDate?.ToString("yyyy/MM/dd"),
                Discount = product.Discount,
                Num = product.Num,
                Order = product.Order,
                Price = product.Price,
                ProductName = product.ProductName,
                Description = product.Description,
                IsNew = product.IsNew,
                LinkProduct = product.LinkProduct,
                Model = product.Model,
                Properties = product.Properties,
                IsEnabled = product.IsEnabled,
                Series = product.Series,
                CategoryThumb = _categoryMapper.MapThumb(product.Category),
                StoreThumb = _storeMapper.MapThumb(product.Store),
                Barcode = product.Barcode,
                CompanyName = product.CompanyName,
                HtmlContent = product.HtmlContent,
                Keywords = product.Keywords
            };

            if (product.ProductImages != null && product.ProductImages.Count > 0)
            {
                response.Images = new List<ProductImageResponse>();
                foreach (var image in product.ProductImages)
                {
                    response.Images.Add(this._productImageMapper.Map(image));
                };
            }
            if (product.HasFile)
                response.FileUrl = $"/KL_ImagesRepo/Files/Products/{product.Id}{product.FileExtention}";
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
