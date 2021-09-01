using System.Linq;
using Kalabean.Domain.Mappers;
using Kalabean.Domain.Repositories;
using Kalabean.Domain.Requests.Category;
using Kalabean.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Kalabean.Domain.Base;
using Kalabean.Domain.Services;
using Kalabean.Infrastructure.Services.Image;
using Kalabean.Infrastructure.Files;
using Kalabean.Infrastructure.AppSettingConfigs.Images;
using Microsoft.Extensions.Options;
using Kalabean.Domain.Requests.ResizeImage;
using System.Drawing;

namespace Kalabean.Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryMapper _categoryMapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IResizeImageService<int> _resizeImageService;
        private readonly KalabeanFileProvider _fileProvider;
        private readonly List<ImageSize> _imageConfig;
        public CategoryService(ICategoryRepository categoryRepository,
                               ICategoryMapper categoryMapper,
                               IUnitOfWork unitOfWork,
                               IFileAccessProvider fileProvider,
                               IResizeImageService<int> resizeImageService,
                               IOptions<ImageSize> ImageConfig)
        {
            _categoryRepository = categoryRepository;
            _categoryMapper = categoryMapper;
            _unitOfWork = unitOfWork;
            _fileProvider = new KalabeanFileProvider(fileProvider);
            this._resizeImageService = resizeImageService;
            _imageConfig = ImageConfig.Value.ImageSizes.Where(x => x.ImageType == ImageType.Category).ToList();
        }

        public async Task<IEnumerable<CategoryResponse>> GetCategoriesAsync(GetCategoriesRequest request)
        {
            var result = await _categoryRepository.Get(request.Name, request.ParentId);
            return result.Select(c => _categoryMapper.Map(c));
        }
        public async Task<CategoryResponse> GetCategoryAsync(GetCategoryRequest request)
        {
            if (request?.Id == null) throw new ArgumentNullException();
            var category = await _categoryRepository.GetById(request.Id);
            return _categoryMapper.Map(category);
        }
        public async Task<CategoryResponse> AddCategoryAsync(AddCategoryRequest request)
        {
            var item = _categoryMapper.Map(request);
            var result = _categoryRepository.Add(item);
            Tuple<bool, string> ImgResult = null;
            if (await _unitOfWork.CommitAsync() > 0 &&
                request.Image != null)
            {
                using (var fileContent = request.Image.OpenReadStream())
                    ImgResult = _fileProvider.SaveCategoryImage(fileContent, result.Id);

                foreach (var ImageResize in _imageConfig)
                {

                    if (ImgResult != null && ImgResult.Item1)
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

            return _categoryMapper.Map(await _categoryRepository.GetById(result.Id));
        }
        public async Task<CategoryResponse> EditCategoryAsync(EditCategoryRequest request)
        {
            var existingRecord = await _categoryRepository.GetById(request.Id);

            if (existingRecord == null)
                throw new ArgumentException($"Entity with {request.Id} is not present");

            var entity = _categoryMapper.Map(request);
            entity.CreatedDate = existingRecord.CreatedDate;
            if (request.ImageEdited)
            {
                if ((entity.HasImage ?? false) || request.Image != null)
                {
                    Tuple<bool, string> ImgResult = null;
                    if (request.Image != null)
                    {
                        using (var fileContent = request.Image.OpenReadStream())
                            ImgResult = _fileProvider.SaveCategoryImage(fileContent, entity.Id);
                        entity.HasImage = true;

                        foreach (var ImageResize in _imageConfig)
                        {
                            if (ImgResult != null && ImgResult.Item1)
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

                }
                else
                {
                    _fileProvider.DeleteCategoryImage(entity.Id);
                    entity.HasImage = false;
                }
            }
            var result = _categoryRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            return _categoryMapper.Map(await _categoryRepository.GetById(result.Id));
        }

        public async Task BatchDeleteCategoriesAsync(int[] ids)
        {
            List<Kalabean.Domain.Entities.Category> categories =
                _categoryRepository.List(c => ids.Contains(c.Id)).ToList();
            foreach (Kalabean.Domain.Entities.Category category in categories)
                category.IsDeleted = true;
            _categoryRepository.UpdateBatch(categories);

            await _unitOfWork.CommitAsync();
        }
    }
}
