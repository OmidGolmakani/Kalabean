using System.Linq;
using Kalabean.Domain.Mappers;
using Kalabean.Domain.Repositories;
using Kalabean.Domain.Requests.PossibilitiesShopCenter;
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
    public class PossibilitiesShopCenterService : IPossibilitiesShopCenterService
    {
        private readonly IPossibilitiesShopCenterRepository _PossibilitiesShopCenterRepository;
        private readonly IPossibilitiesShopCenterMapper _PossibilitiesShopCenterMapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly KalabeanFileProvider _fileProvider;
        private readonly IResizeImageService<long> _resizeImageService;
        private readonly List<ImageSize> _imageConfig;
        public PossibilitiesShopCenterService(IPossibilitiesShopCenterRepository PossibilitiesShopCenterRepository,
                              IPossibilitiesShopCenterMapper PossibilitiesShopCenterMapper,
                              IUnitOfWork unitOfWork,
                              KalabeanFileProvider fileProvider,
                              IOptions<ImageSize> imageConfig,
                              IResizeImageService<long> resizeImageService)
        {
            _PossibilitiesShopCenterRepository = PossibilitiesShopCenterRepository;
            _PossibilitiesShopCenterMapper = PossibilitiesShopCenterMapper;
            _unitOfWork = unitOfWork;
            _resizeImageService = resizeImageService;
            _imageConfig = imageConfig.Value.ImageSizes.Where(x => x.ImageType == ImageType.PossibilitiesShopCenter).ToList();
            this._fileProvider = fileProvider;
        }

        public async Task<ListPagingResponse<PossibilitiesShopCenterResponse>> GetPossibilitiesShopCentersAsync(GetPossibilitiesShopCentersRequest request)
        {
            var result = await _PossibilitiesShopCenterRepository.Get(request);
            var list = result.Select(c => _PossibilitiesShopCenterMapper.Map(c));
            var count = await _PossibilitiesShopCenterRepository.Count(request);
            return new ListPagingResponse<PossibilitiesShopCenterResponse>() { Items = list, Total = count };
        }
        public async Task<PossibilitiesShopCenterResponse> GetPossibilitiesShopCenterAsync(GetPossibilitiesShopCenterRequest request)
        {
            if (request?.Id == null) throw new ArgumentNullException();
            var PossibilitiesShopCenter = await _PossibilitiesShopCenterRepository.GetById(request.Id);
            return _PossibilitiesShopCenterMapper.Map(PossibilitiesShopCenter);
        }
        public async Task<PossibilitiesShopCenterResponse> AddPossibilitiesShopCenterAsync(AddPossibilitiesShopCenterRequest request)
        {
            var item = _PossibilitiesShopCenterMapper.Map(request);
            item.HasImage = request.Image != null;
            var result = _PossibilitiesShopCenterRepository.Add(item);
            Tuple<bool, string> imgResult = null;
            var Commit = await _unitOfWork.CommitAsync();
            if (Commit > 0 &&
                request.Image != null)
            {
                using (var fileContent = request.Image.OpenReadStream())
                    imgResult = _fileProvider.SavePossibilitiesShopImage(fileContent, result.Id);
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

            return await this.GetPossibilitiesShopCenterAsync(new GetPossibilitiesShopCenterRequest { Id = result.Id });
        }
        public async Task<PossibilitiesShopCenterResponse> EditPossibilitiesShopCenterAsync(EditPossibilitiesShopCenterRequest request)
        {
            var existingRecord = await _PossibilitiesShopCenterRepository.GetById(request.Id);

            if (existingRecord == null)
                throw new ArgumentException($"Entity with {request.Id} is not present");

            var entity = _PossibilitiesShopCenterMapper.Map(request);
            entity.HasImage = entity.HasImage || (!request.ImageEdited && existingRecord.HasImage);
            if (request.ImageEdited)
            {
                if (entity.HasImage || request.Image != null)
                {

                    Tuple<bool, string> ImgResult = null;
                    if (request.Image != null)
                    {
                        using (var fileContent = request.Image.OpenReadStream())
                            ImgResult = _fileProvider.SavePossibilitiesShopImage(fileContent, entity.Id);
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
                    _fileProvider.DeletePossibilitiesShoppingCenterImage(entity.Id);
                    entity.HasImage = false;
                }
            }



            var result = _PossibilitiesShopCenterRepository.Update(entity);
            await _unitOfWork.CommitAsync();

            return _PossibilitiesShopCenterMapper.Map(await _PossibilitiesShopCenterRepository.GetById(result.Id));
        }

        public async Task BatchDeletePossibilitiesShopCentersAsync(int[] ids)
        {
            List<Kalabean.Domain.Entities.PossibilitiesShopCenter> PossibilitiesShopCenters =
                _PossibilitiesShopCenterRepository.List(c => ids.Contains(c.Id)).ToList();
            foreach (Kalabean.Domain.Entities.PossibilitiesShopCenter PossibilitiesShopCenter in PossibilitiesShopCenters)
                PossibilitiesShopCenter.IsDeleted = true;
            _PossibilitiesShopCenterRepository.UpdateBatch(PossibilitiesShopCenters);

            await _unitOfWork.CommitAsync();
        }

        public async Task<int> Count(GetPossibilitiesShopCentersRequest request)
        {
            if (request == null) throw new ArgumentNullException();
            var PossibilitiesShopCenter = await _PossibilitiesShopCenterRepository.Count(request);
            return PossibilitiesShopCenter;
        }
    }
}
