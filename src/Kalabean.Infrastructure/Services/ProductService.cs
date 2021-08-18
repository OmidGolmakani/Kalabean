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

            if (await _unitOfWork.CommitAsync() > 0 &&
               request.Images != null && request.Images.Count() != 0)
            {
                foreach (var img in request.Images)
                {
                    using (var fileContent = img.OpenReadStream())
                        ImgResult = _fileProvider.SaveProductImage(fileContent, result.Id);

                    foreach (var ImageResize in _imageConfig)
                    {
                        if (ImgResult.Item1)
                        {
                            await _resizeImageService.Resize(new GetImageRequest<long>()
                            {
                                Id = result.Id,
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
        public async Task<ProductResponse> EditProductAsync(EditProductRequest request)
        {
            var existingRecord = await _ProductRepository.GetById(request.Id);

            if (existingRecord == null)
                throw new ArgumentException($"Entity with {request.Id} is not present");

            var entity = _ProductMapper.Map(request);
            var result = _ProductRepository.Update(entity);
            await _unitOfWork.CommitAsync();
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
