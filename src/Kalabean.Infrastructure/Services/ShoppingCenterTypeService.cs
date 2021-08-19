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
using Kalabean.Infrastructure.Services.Image;
using Kalabean.Infrastructure.AppSettingConfigs.Images;
using Microsoft.Extensions.Options;
using Kalabean.Domain.Requests.ResizeImage;
using System.Drawing;

namespace Kalabean.Infrastructure.Services
{
    public class ShoppingCenterTypeService : IShoppingCenterTypeService
    {
        private readonly IShoppingCenterTypeRepository _typeRepository;
        private readonly IShoppingCenterTypeMapper _typeMapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IResizeImageService<int> _resizeImageService;
        private readonly KalabeanFileProvider _fileProvider;
        

        private readonly List<ImageSize> _imageConfig;

        public ShoppingCenterTypeService(IShoppingCenterTypeRepository typeRepository,
            IShoppingCenterTypeMapper typeMapper,
            IUnitOfWork unitOfWork,
            IResizeImageService<int> imageService,
            IOptions<ImageSize> ImageConfig,
            IFileAccessProvider fileProvider)
        {
            _typeRepository = typeRepository;
            _typeMapper = typeMapper;
            _unitOfWork = unitOfWork;
            _resizeImageService = imageService;
            _imageConfig = ImageConfig.Value.ImageSizes.Where(x => x.ImageType == ImageType.ShoppingCenterTypes).ToList();
            _fileProvider = new KalabeanFileProvider(fileProvider);
        }

        public async Task<IEnumerable<ShoppingCenterTypeResponse>> GetTypesAsync()
        {
            var result = _typeRepository.List(c => !c.IsDeleted);
            return result.Select(c => _typeMapper.Map(c));
        }
        public async Task<ShoppingCenterTypeResponse> GetTypeAsync(GetShoppingCenterTypeRequest request)
        {
            if (request?.Id == null) throw new ArgumentNullException();
            var city = await _typeRepository.GetById(request.Id);
            return _typeMapper.Map(city);
        }
        public async Task<ShoppingCenterTypeResponse> AddTypeAsync(AddShoppingCenterTypeRequest request)
        {
            var item = _typeMapper.Map(request);
            item.HasImage = request.Image != null;
            var result = _typeRepository.Add(item);
            Tuple<bool, string> ImgResult = null;
            if (await _unitOfWork.CommitAsync() > 0 &&
                            request.Image != null)
            {
                using (var fileContent = request.Image.OpenReadStream())
                    ImgResult = _fileProvider.SaveTypeImage(fileContent, result.Id);
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
            return _typeMapper.Map(await _typeRepository.GetById(result.Id));
        }
        public async Task<ShoppingCenterTypeResponse> EditTypeAsync(EditShoppingCenterTypeRequest request)
        {
            var existingRecord = await _typeRepository.GetById(request.Id);

            if (existingRecord == null)
                throw new ArgumentException($"Entity with {request.Id} is not present");

            var entity = _typeMapper.Map(request);
            entity.HasImage = entity.HasImage || (!request.ImageEdited && existingRecord.HasImage);
            if (request.ImageEdited)
            {
                if (entity.HasImage || request.Image != null)
                {

                    Tuple<bool, string> ImgResult = null;
                    if (request.Image != null)
                    {
                        using (var fileContent = request.Image.OpenReadStream())
                            ImgResult = _fileProvider.SaveTypeImage(fileContent, entity.Id);
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
                        _fileProvider.DeleteTypeImage(entity.Id);
                        entity.HasImage = false;
                    }
                }
            }

            var result = _typeRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            return _typeMapper.Map(await _typeRepository.GetById(result.Id));
        }

        public async Task BatchDeleteTypesAsync(int[] ids)
        {
            List<Kalabean.Domain.Entities.ShoppingCenterType> cities =
                _typeRepository.List(c => ids.Contains(c.Id)).ToList();
            foreach (Kalabean.Domain.Entities.ShoppingCenterType city in cities)
                city.IsDeleted = true;
            _typeRepository.UpdateBatch(cities);

            await _unitOfWork.CommitAsync();
        }
    }
}
