using System.Linq;
using Kalabean.Domain.Mappers;
using Kalabean.Domain.Repositories;
using Kalabean.Domain.Requests.Product;
using Kalabean.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Kalabean.Domain.Base;
using Kalabean.Domain.Services;
using Kalabean.Infrastructure.Files;
using Kalabean.Infrastructure.Services.Image;
using Kalabean.Infrastructure.AppSettingConfigs.Images;
using Microsoft.Extensions.Options;
using Kalabean.Domain.Requests.ResizeImage;
using System.Drawing;
using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.ProductImage;

namespace Kalabean.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _ProductRepository;
        private readonly IProductImageRepository _ProductImageRepository;
        private readonly IProductMapper _ProductMapper;
        private readonly IProductImageMapper _ProductImageMapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IResizeImageService<long> _resizeImageService;
        private readonly KalabeanFileProvider _fileProvider;

        private readonly List<ImageSize> _imageConfig;

        public ProductService(IProductRepository ProductRepository,
                              IProductImageRepository ProductImageRepository,
                              IProductMapper ProductMapper,
                              IProductImageMapper ProductImageMapper,
                              IUnitOfWork unitOfWork,
                              KalabeanFileProvider fileProvider,
                              IResizeImageService<long> imageService,
                           IOptions<ImageSize> ImageConfig,
                           IResizeImageService<long> resizeImageService)
        {
            _ProductRepository = ProductRepository;
            _ProductImageRepository = ProductImageRepository;
            _ProductMapper = ProductMapper;
            _ProductImageMapper = ProductImageMapper;
            _unitOfWork = unitOfWork;
            _resizeImageService = resizeImageService;
            _imageConfig = ImageConfig.Value.ImageSizes.Where(x => x.ImageType == ImageType.Product).ToList();
            _fileProvider = fileProvider;
        }

        public async Task<ListPagingResponse<ProductResponse>> GetProductsAsync(GetProductsRequest request)
        {
            var result = await _ProductRepository.Get(request);
            var list = result.Select(p => _ProductMapper.Map(p));
            var count = await _ProductRepository.Count(request);
            return new ListPagingResponse<ProductResponse>() { Items = list, Total = count };
        }
        public async Task<ProductResponse> GetProductAsync(GetProductRequest request)
        {
            if (request?.Id == null) throw new ArgumentNullException();
            var Product = await _ProductRepository.GetById(request.Id);
            return _ProductMapper.Map(Product);
        }
        public async Task<ProductResponse> AddProductAsync(AddProductRequest request)
        {
            var item = _ProductMapper.Map(request);
            var result = _ProductRepository.Add(item);

            Tuple<bool, string> ImgResult = null;
            int commited = await _unitOfWork.CommitAsync();
            if (commited > 0 &&
               item.ProductImages != null && item.ProductImages.Count > 0 &&
               request.Images.Count() == item.ProductImages.Count)
            {
                int totalIndex = item.ProductImages.Count;
                var productImages = item.ProductImages;
                foreach (var productImage in productImages)
                {
                    using (var fileContent = request.Images.ElementAt(--totalIndex).OpenReadStream())
                        ImgResult = _fileProvider.SaveProductImage(fileContent, productImage.Id);

                    foreach (var ImageResize in _imageConfig)
                    {
                        if (ImgResult.Item1)
                        {
                            await _resizeImageService.Resize(new GetImageRequest<long>()
                            {
                                Id = productImage.Id,
                                ImageSize = new Size(ImageResize.Width, ImageResize.Height),
                                ImageUrl = ImgResult.Item2,
                                Folder = string.Format("{0}_{1}", ImageResize.Width, ImageResize.Height)
                            });
                        }
                    }
                }
            }

            if (commited > 0 &&
                request.File != null)
            {
                using (var fileContent = request.File.OpenReadStream())
                    _fileProvider.SaveProductFile(fileContent, result.Id, item.FileExtention);
            }
            return _ProductMapper.Map(await _ProductRepository.GetById(result.Id));
        }
        public async Task<ProductResponse> EditProductAsync(EditProductRequest request)
        {
            var existingRecord = await _ProductRepository.GetById(request.Id);

            if (existingRecord == null)
                throw new ArgumentException($"Entity with {request.Id} is not present");
            var entity = _ProductMapper.Map(request);
            entity.CreatedDate = existingRecord.CreatedDate;
            List<ProductImage> addedImages = null;
            if (request.Images != null && request.Images.Count() > 0)
            {
                addedImages = new List<ProductImage>();
                foreach (var image in request.Images)
                {
                    addedImages.Add( _ProductImageRepository.Add(this._ProductImageMapper.
                    Map(new AddProductImageRequest
                    {
                        ProductId = existingRecord.Id
                    })));
                }
            }
            if (request.Removed != null && request.Removed.Count() > 0)
            {
                foreach (int i in request.Removed)
                {
                    _ProductImageRepository.Delete(new ProductImage { Id = i });
                    _fileProvider.DeleteProductImage(i);
                }
            }
            entity.HasFile = entity.HasFile || (!request.FileEdited && existingRecord.HasFile);
            var result = _ProductRepository.Update(entity);
            int commited = await _unitOfWork.CommitAsync();

            if (commited > 0 &&
                request.File != null)
            {
                using (var fileContent = request.File.OpenReadStream())
                    _fileProvider.SaveProductFile(fileContent, result.Id, result.FileExtention);
            }
            if (commited > 0 &&
                addedImages != null)
            {
                int totalIndex = addedImages.Count;
                foreach (var image in addedImages)
                {
                    Tuple<bool, string> ImgResult = null;
                    using (var fileContent = request.Images.ElementAt(--totalIndex).OpenReadStream())
                        ImgResult = _fileProvider.SaveProductImage(fileContent, image.Id);
                    foreach (var ImageResize in _imageConfig)
                    {
                        if (ImgResult.Item1)
                        {
                            await _resizeImageService.Resize(new GetImageRequest<long>()
                            {
                                Id = image.Id,
                                ImageSize = new Size(ImageResize.Width, ImageResize.Height),
                                ImageUrl = ImgResult.Item2,
                                Folder = string.Format("{0}_{1}", ImageResize.Width, ImageResize.Height)
                            });
                        }
                    }
                }
                    
            }
            return _ProductMapper.Map(await _ProductRepository.GetById(result.Id));
        }

        public async Task BatchDeleteProductsAsync(long[] ids)
        {
            List<Kalabean.Domain.Entities.Product> Products =
                _ProductRepository.List(c => ids.Contains(c.Id)).ToList();
            foreach (Kalabean.Domain.Entities.Product Product in Products)
                Product.IsDeleted = true;
            _ProductRepository.UpdateBatch(Products);

            await _unitOfWork.CommitAsync();
        }
    }
}
