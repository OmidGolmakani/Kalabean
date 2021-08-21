using System.Linq;
using Kalabean.Domain.Mappers;
using Kalabean.Domain.Repositories;
using Kalabean.Domain.Requests.Advertise;
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
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace Kalabean.Infrastructure.Services
{
    public class AdvertiseService : IAdvertiseService
    {
        private readonly IAdvertiseRepository _AdvertiseRepository;
        private readonly IAdvertiseMapper _AdvertiseMapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly KalabeanFileProvider _fileProvider;
        private readonly IResizeImageService<long> _resizeImageService;
        private readonly List<ImageSize> _imageConfig;
        public AdvertiseService(IAdvertiseRepository AdvertiseRepository,
                              IAdvertiseMapper AdvertiseMapper,
                              IUnitOfWork unitOfWork,
                              KalabeanFileProvider fileProvider,
                              IOptions<ImageSize> imageConfig,
                              IResizeImageService<long> resizeImageService)
        {
            _AdvertiseRepository = AdvertiseRepository;
            _AdvertiseMapper = AdvertiseMapper;
            _unitOfWork = unitOfWork;
            _resizeImageService = resizeImageService;
            _imageConfig = imageConfig.Value.ImageSizes.Where(x => x.ImageType == ImageType.Advertise).ToList();
            this._fileProvider = fileProvider;
        }

        public async Task<ListPagingResponse<AdvertiseResponse>> GetAdvertisingAsync(GetAdvertisingRequest request)
        {
            var result = await _AdvertiseRepository.Get(request);
            var list = result.Select(c => _AdvertiseMapper.Map(c));
            var count = await _AdvertiseRepository.Count(request);
            return new ListPagingResponse<AdvertiseResponse>() { Items = list, Total = count };
        }
        public async Task<AdvertiseResponse> GetAdvertiseAsync(GetAdvertiseRequest request)
        {
            if (request?.Id == null) throw new ArgumentNullException();
            var Advertise = await _AdvertiseRepository.GetById(request.Id);
            return _AdvertiseMapper.Map(Advertise);
        }
        public async Task<List<AdvertisePositionResponse>> GetAdvertisePositionAsync()
        {
            List<AdvertisePositionResponse> list = new List<AdvertisePositionResponse>();
            foreach (Domain.Entities.AdPositions item in Enum.GetValues(typeof(Domain.Entities.AdPositions)).Cast<Domain.Entities.AdPositions>()) 
            {
                list.Add(new AdvertisePositionResponse()
                {
                    Id = (byte)item,
                    Name = GetDisplayName(item)
                });
            }
            return list;
        }
        public async Task<AdvertiseResponse> AddAdvertiseAsync(AddAdvertiseRequest request)
        {
            var item = _AdvertiseMapper.Map(request);
            item.HasImage = request.Image != null;
            var result = _AdvertiseRepository.Add(item);
            Tuple<bool, string> imgResult = null;
            var Commit = await _unitOfWork.CommitAsync();
            if (Commit > 0 &&
                request.Image != null)
            {
                using (var fileContent = request.Image.OpenReadStream())
                    imgResult = _fileProvider.SaveAdvertiseImage(fileContent, result.Id);
                foreach (var ImageResize in _imageConfig)
                {

                    if (imgResult.Item1)
                    {
                        await _resizeImageService.Resize(new GetImageRequest<long>()
                        {
                            Id = result.Id,
                            ImageSize = new Size(ImageResize.Width, ImageResize.Height),
                            ImageUrl = imgResult.Item2,
                            Folder = string.Format("{0}_{1}", ImageResize.Width, ImageResize.Height)
                        });
                    }
                }
            }

            return await this.GetAdvertiseAsync(new GetAdvertiseRequest { Id = result.Id });
        }
        public async Task<AdvertiseResponse> EditAdvertiseAsync(EditAdvertiseRequest request)
        {
            var existingRecord = await _AdvertiseRepository.GetById(request.Id);

            if (existingRecord == null)
                throw new ArgumentException($"Entity with {request.Id} is not present");

            var entity = _AdvertiseMapper.Map(request);
            entity.HasImage = entity.HasImage || (!request.ImageEdited && existingRecord.HasImage);
            if (request.ImageEdited)
            {
                if (entity.HasImage || request.Image != null)
                {

                    Tuple<bool, string> ImgResult = null;
                    if (request.Image != null)
                    {
                        using (var fileContent = request.Image.OpenReadStream())
                            ImgResult = _fileProvider.SaveAdvertiseImage(fileContent, entity.Id);
                        entity.HasImage = true;

                        foreach (var ImageResize in _imageConfig)
                        {

                            if (ImgResult.Item1)
                            {
                                await _resizeImageService.Resize(new GetImageRequest<long>()
                                {
                                    Id = existingRecord.Id,
                                    ImageSize = new Size(ImageResize.Width, ImageResize.Height),
                                    ImageUrl = ImgResult.Item2,
                                    Folder = string.Format("{0}_{1}", ImageResize.Width, ImageResize.Height)
                                });
                            }
                        }
                    }

                }
                else
                {
                    _fileProvider.DeleteAdvertiseImage(entity.Id);
                    entity.HasImage = false;
                }
            }



            var result = _AdvertiseRepository.Update(entity);
            await _unitOfWork.CommitAsync();

            return _AdvertiseMapper.Map(await _AdvertiseRepository.GetById(result.Id));
        }

        public async Task BatchDeleteAdvertisingAsync(int[] ids)
        {
            List<Kalabean.Domain.Entities.Advertise> Advertises =
                _AdvertiseRepository.List(c => ids.Contains(c.Id)).ToList();
            foreach (Kalabean.Domain.Entities.Advertise Advertise in Advertises)
                Advertise.IsDeleted = true;
            _AdvertiseRepository.UpdateBatch(Advertises);

            await _unitOfWork.CommitAsync();
        }

        public async Task<long> Count(GetAdvertisingRequest request)
        {
            if (request == null) throw new ArgumentNullException();
            var Advertise = await _AdvertiseRepository.Count(request);
            return Advertise;
        }
        private string GetDisplayName(Enum enumValue)
        {
            string displayName;
            displayName = enumValue.GetType()
                .GetMember(enumValue.ToString())
                .FirstOrDefault()
                .GetCustomAttribute<DisplayAttribute>()?
                .GetName();
            if (String.IsNullOrEmpty(displayName))
            {
                displayName = enumValue.ToString();
            }
            return displayName;
        }
    }
}
