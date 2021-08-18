using System.Linq;
using Kalabean.Domain.Mappers;
using Kalabean.Domain.Repositories;
using Kalabean.Domain.Requests.ShoppingCenter;
using Kalabean.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Kalabean.Domain.Base;
using Kalabean.Domain.Services;
using Kalabean.Infrastructure.Files;
using Microsoft.EntityFrameworkCore;
using Kalabean.Infrastructure.AppSettingConfigs.Images;
using Kalabean.Infrastructure.Services.Image;
using Microsoft.Extensions.Options;
using Kalabean.Domain.Requests.ResizeImage;
using System.Drawing;

namespace Kalabean.Infrastructure.Services
{
    public class ShoppingCenterService : IShoppingCenterService
    {
        private readonly IShoppingCenterRepository _shoppingRepository;
        private readonly IShoppingCenterMapper _shoppingMapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IResizeImageService<int> _resizeImageService;
        private readonly KalabeanFileProvider _fileProvider;

        private readonly List<ImageSize> _imageConfig;
        public ShoppingCenterService(IShoppingCenterRepository shoppingRepository,
            IShoppingCenterMapper shoppingMapper,
            IUnitOfWork unitOfWork,
            IResizeImageService<int> imageService,
            IOptions<ImageSize> ImageConfig,
            IResizeImageService<int> ResizeImageService,
            IFileAccessProvider fileProvider)
        {
            _shoppingMapper = shoppingMapper;
            _shoppingRepository = shoppingRepository;
            _unitOfWork = unitOfWork;
            _resizeImageService = ResizeImageService;
            _imageConfig = ImageConfig.Value.ImageSizes.Where(x => x.ImageType == ImageType.ShoppingCenter).ToList();
            _fileProvider = new KalabeanFileProvider(fileProvider);
        }

        public async Task<ListPagingResponse<ShoppingCenterResponse>> GetShoppingCentersAsync(GetShopingCentersRequest request)
        {
            var result = await _shoppingRepository.Get(request);
            var list = result.Select(c => _shoppingMapper.Map(c));
            return new ListPagingResponse<ShoppingCenterResponse>()
            {
                Items = list,
                Total = await _shoppingRepository.Count(request)
            };
        }
        public async Task<ShoppingCenterResponse> GetShoppingCenterAsync(GetShoppingCenterRequest request)
        {
            if (request?.Id == null) throw new ArgumentNullException();
            return _shoppingMapper.Map(await _shoppingRepository.GetById(request.Id));
        }
        public async Task<ShoppingCenterResponse> AddShoppingCenterAsync(AddShoppingCenterRequest request)
        {
            var item = _shoppingMapper.Map(request);
            item.HasImage = request.Image != null;
            var result = _shoppingRepository.Add(item);
            Tuple<bool, string> ImgResult = null;
            if (await _unitOfWork.CommitAsync() > 0 &&
                request.Image != null)
            {
                using (var fileContent = request.Image.OpenReadStream())
                    ImgResult = _fileProvider.SaveShoppingCenterImage(fileContent, result.Id);
                foreach (var ImageResize in _imageConfig)
                {

                    if (ImgResult.Item1)
                    {
                        await _resizeImageService.Resize(new GetImageRequest<int>()
                        {
                            Id = result.Id,
                            ImageSize = new Size(ImageResize.Width, ImageResize.Height),
                            ImageUrl = ImgResult.Item2,
                            Folder = string.Format("{0}_{1}", ImageResize.Width, ImageResize.Height)
                        });
                    }
                }
            }
            return _shoppingMapper.Map(await _shoppingRepository.GetById(result.Id));
        }
        public async Task<ShoppingCenterResponse> EditShoppingCenterAsync(EditShoppingCenterRequest request)
        {
            var existingRecord = await _shoppingRepository.GetById(request.Id);

            if (existingRecord == null)
                throw new ArgumentException($"Entity with {request.Id} is not present");

            var entity = _shoppingMapper.Map(request);
            entity.HasImage = entity.HasImage || (!request.ImageEdited && existingRecord.HasImage);
            if (entity.HasImage || request.Image != null)
            {
                if (request.ImageEdited)
                {
                    Tuple<bool, string> ImgResult = null;
                    if (request.Image != null)
                    {
                        using (var fileContent = request.Image.OpenReadStream())
                            ImgResult = _fileProvider.SaveShoppingCenterImage(fileContent, entity.Id);
                        entity.HasImage = true;

                        foreach (var ImageResize in _imageConfig)
                        {
                            if (ImgResult.Item1)
                            {
                                await _resizeImageService.Resize(new GetImageRequest<int>()
                                {
                                    Id = existingRecord.Id,
                                    ImageSize = new Size(ImageResize.Width, ImageResize.Height),
                                    ImageUrl = ImgResult.Item2,
                                    Folder = string.Format("{0}_{1}", ImageResize.Width, ImageResize.Height)
                                });
                            }
                        }
                    }
                    else
                    {
                        _fileProvider.DeleteShoppingCenterImage(entity.Id);
                        entity.HasImage = false;
                    }
                }
            }
            var result = _shoppingRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            return _shoppingMapper.Map(await _shoppingRepository.GetById(result.Id));
        }

        public async Task BatchDeleteShoppingCentersAsync(int[] ids)
        {
            List<Kalabean.Domain.Entities.ShoppingCenter> shoppings =
                _shoppingRepository.List(c => ids.Contains(c.Id)).ToList();
            foreach (Kalabean.Domain.Entities.ShoppingCenter shopping in shoppings)
                shopping.IsDeleted = true;
            _shoppingRepository.UpdateBatch(shoppings);

            await _unitOfWork.CommitAsync();
        }
    }
}
