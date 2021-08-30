using System.Linq;
using Kalabean.Domain.Mappers;
using Kalabean.Domain.Repositories;
using Kalabean.Domain.Requests.Requirement;
using Kalabean.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Kalabean.Domain.Base;
using Kalabean.Domain.Services;
using Kalabean.Domain.Entities;
using System.Drawing;
using Microsoft.Extensions.Options;
using Kalabean.Infrastructure.Services.Image;
using Kalabean.Infrastructure.Files;
using Kalabean.Infrastructure.AppSettingConfigs.Images;
using Kalabean.Domain.Requests.ResizeImage;
using Microsoft.AspNetCore.Identity;

namespace Kalabean.Infrastructure.Services
{
    public class RequirementService : IRequirementService
    {
        private readonly IRequirementRepository _RequirementRepository;
        private readonly IRequirementMapper _RequirementMapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IResizeImageService<long> _resizeImageService;
        private readonly KalabeanFileProvider _fileProvider;
        private readonly List<ImageSize> _imageConfig;
        private readonly IRequirementUserSeenRepository _requirementSeen;
        private readonly UserManager<User> _UserManager;

        public RequirementService(IRequirementRepository RequirementRepository,
                                  IRequirementMapper RequirementMapper,
                                   IUnitOfWork unitOfWork,
                                   IFileAccessProvider fileProvider,
                                   IResizeImageService<long> resizeImageService,
                                   IOptions<ImageSize> ImageConfig,
                                   IRequirementUserSeenRepository requirementSeen,
                                   UserManager<Domain.Entities.User> userManager)
        {
            _RequirementRepository = RequirementRepository;
            _RequirementMapper = RequirementMapper;
            _unitOfWork = unitOfWork;
            _fileProvider = new KalabeanFileProvider(fileProvider);
            this._resizeImageService = resizeImageService;
            this._requirementSeen = requirementSeen;
            this._UserManager = userManager;
            _imageConfig = ImageConfig.Value.ImageSizes.Where(x => x.ImageType == ImageType.Requirement).ToList();
        }

        public async Task<ListPagingResponse<RequirementResponse>> GetRequirementsAsync(GetRequirementsRequest request)
        {
            var UserId = Helpers.JWTTokenManager.GetUserIdByToken();
            var user = _UserManager.Users.FirstOrDefault(u => u.Id == UserId);
            var userRoles = await _UserManager.GetRolesAsync(user);
            if (userRoles.FirstOrDefault(u => u == "Administrator") == null)
            {
                request.UserId = UserId;
            }
            var result = await _RequirementRepository.Get(request);
            var list = result.Select(p => _RequirementMapper.Map(p));
            var count = await _RequirementRepository.Count(request);
            return new ListPagingResponse<RequirementResponse>() { Items = list, Total = count };
        }
        public async Task<RequirementResponse> GetRequirementAsync(GetRequirementRequest request)
        {
            if (request?.Id == null) throw new ArgumentNullException();
            var Requirement = await _RequirementRepository.GetById(request.Id);
            _requirementSeen.Add(new RequirementUserSeen()
            {
                UserId = Helpers.JWTTokenManager.GetUserIdByToken(),
                RequirementId = request.Id
            });
            await _unitOfWork.CommitAsync();
            return _RequirementMapper.Map(Requirement);
        }
        public async Task<RequirementResponse> AddRequirementAsync(AddRequirementRequest request)
        {
            var item = _RequirementMapper.Map(request);
            item.UserId = Helpers.JWTTokenManager.GetUserIdByToken();
            item.RequirementStatus = (byte)RequirementStatus.AwaitingApproval;
            item.Exprie = DateTime.Now.AddHours(48).Minute;
            var result = _RequirementRepository.Add(item);
            Tuple<bool, string> ImgResult = null;
            if (await _unitOfWork.CommitAsync() > 0 &&
                request.Image != null)
            {
                using (var fileContent = request.Image.OpenReadStream())
                    ImgResult = _fileProvider.SaveRequirementImage(fileContent, result.Id);

                foreach (var ImageResize in _imageConfig)
                {

                    if (ImgResult != null && ImgResult.Item1)
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

            return _RequirementMapper.Map(await _RequirementRepository.GetById(result.Id));
        }
        public async Task<RequirementResponse> EditRequirementAsync(EditRequirementRequest request)
        {
            var existingRecord = await _RequirementRepository.GetById(request.Id);

            if (existingRecord == null)
                throw new ArgumentException($"Entity with {request.Id} is not present");

            var entity = _RequirementMapper.Map(request);
            entity.UserId = Helpers.JWTTokenManager.GetUserIdByToken();
            if (request.ImageEdited)
            {
                if (entity.HasImage || request.Image != null)
                {
                    Tuple<bool, string> ImgResult = null;
                    if (request.Image != null)
                    {
                        using (var fileContent = request.Image.OpenReadStream())
                            ImgResult = _fileProvider.SaveOrderImage(fileContent, entity.Id);
                        entity.HasImage = true;

                        foreach (var ImageResize in _imageConfig)
                        {
                            if (ImgResult != null && ImgResult.Item1)
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
                    _fileProvider.DeleteReqirementImage(entity.Id);
                    entity.HasImage = false;
                }
            }
            var result = _RequirementRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            return _RequirementMapper.Map(await _RequirementRepository.GetById(result.Id));
        }

        public async Task BatchDeleteRequirementsAsync(long[] ids)
        {
            List<Kalabean.Domain.Entities.Requirement> Requirements =
                _RequirementRepository.List(c => ids.Contains(c.Id)).ToList();
            foreach (Kalabean.Domain.Entities.Requirement Requirement in Requirements)
                Requirement.IsDeleted = true;
            _RequirementRepository.UpdateBatch(Requirements);

            await _unitOfWork.CommitAsync();
        }

        public async Task ChangeStatus(long Id, RequirementStatus status)
        {
            await _RequirementRepository.ChangeStatus(Id, status);
            await _unitOfWork.CommitAsync();
        }
    }
}
