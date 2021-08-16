using System.Linq;
using Kalabean.Domain.Mappers;
using Kalabean.Domain.Repositories;
using Kalabean.Domain.Requests.Article;
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
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _ArticleRepository;
        private readonly IArticleMapper _ArticleMapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly KalabeanFileProvider _fileProvider;
        private readonly IResizeImageService<long> _resizeImageService;
        private readonly List<ImageSize> _imageConfig;
        public ArticleService(IArticleRepository ArticleRepository,
                              IArticleMapper ArticleMapper,
                              IUnitOfWork unitOfWork,
                              KalabeanFileProvider fileProvider,
                              IOptions<ImageSize> imageConfig,
                              IResizeImageService<long>  resizeImageService)
        {
            _ArticleRepository = ArticleRepository;
            _ArticleMapper = ArticleMapper;
            _unitOfWork = unitOfWork;
            _resizeImageService = resizeImageService;
            _imageConfig = imageConfig.Value.ImageSizes.Where(x => x.ImageType == ImageType.Article).ToList();
            this._fileProvider = fileProvider;
        }

        public async Task<ListPageingResponse<ArticleResponse>> GetArticlesAsync(GetArticlesRequest request)
        {
            var result = await _ArticleRepository.Get(request);
            var list = result.Select(c => _ArticleMapper.Map(c));
            var count = await _ArticleRepository.Count(request);
            return new ListPageingResponse<ArticleResponse>() { Items = list, RecordCount = count };
        }
        public async Task<ArticleResponse> GetArticleAsync(GetArticleRequest request)
        {
            if (request?.Id == null) throw new ArgumentNullException();
            var Article = await _ArticleRepository.GetById(request.Id);
            return _ArticleMapper.Map(Article);
        }
        public async Task<ArticleResponse> AddArticleAsync(AddArticleRequest request)
        {
            request.AdminId = Helpers.JWTTokenManager.GetUserIdByToken();
            var item = _ArticleMapper.Map(request);
            item.HasImage = request.Image != null;
            var result = _ArticleRepository.Add(item);
            Tuple<bool, string> imgResult = null;
            var Commit = await _unitOfWork.CommitAsync();
            if (Commit > 0 &&
                request.Image != null)
            {
                using (var fileContent = request.Image.OpenReadStream())
                    imgResult = _fileProvider.SaveArticleImage(fileContent, result.Id);
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
            if (Commit > 0 &&
                request.File != null)
            {
                using (var fileContent = request.File.OpenReadStream())
                    _fileProvider.SaveArticleFile(fileContent, result.Id, item.FileExtention);
            }
            return await this.GetArticleAsync(new GetArticleRequest { Id = result.Id });
        }
        public async Task<ArticleResponse> EditArticleAsync(EditArticleRequest request)
        {
            var existingRecord = await _ArticleRepository.GetById(request.Id);

            if (existingRecord == null)
                throw new ArgumentException($"Entity with {request.Id} is not present");

            var entity = _ArticleMapper.Map(request);
            if (entity.HasImage || request.Image != null)
            {
                if (request.ImageEdited)
                {
                    Tuple<bool, string> ImgResult = null;
                    if (request.Image != null)
                    {
                        using (var fileContent = request.Image.OpenReadStream())
                            ImgResult = _fileProvider.SaveArticleImage(fileContent, entity.Id);
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
                    else
                    {
                        _fileProvider.DeleteArticleImage(entity.Id);
                        entity.HasImage = false;
                    }
                }
            }

            if (entity.HasFile || request.File != null)
            {
                if (request.FileEdited)
                {
                    if (request.File != null)
                    {
                        using (var fileContent = request.File.OpenReadStream())
                            _fileProvider.SaveArticleFile(fileContent, entity.Id, entity.FileExtention);
                        entity.HasFile = true;
                    }
                    else
                    {
                        _fileProvider.DeleteArticleFile(entity.Id, entity.FileExtention);
                        entity.HasFile = false;
                    }
                }
            }
            var result = _ArticleRepository.Update(entity);
            await _unitOfWork.CommitAsync();

            return _ArticleMapper.Map(await _ArticleRepository.GetById(result.Id));
        }

        public async Task BatchDeleteArticlesAsync(long[] ids)
        {
            List<Kalabean.Domain.Entities.Article> Articles =
                _ArticleRepository.List(c => ids.Contains(c.Id)).ToList();
            foreach (Kalabean.Domain.Entities.Article Article in Articles)
                Article.IsDeleted = true;
            _ArticleRepository.UpdateBatch(Articles);

            await _unitOfWork.CommitAsync();
        }

        public async Task<long> Count(GetArticlesRequest request)
        {
            if (request == null) throw new ArgumentNullException();
            var Article = await _ArticleRepository.Count(request);
            return Article;
        }
    }
}
