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

namespace Kalabean.Infrastructure.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _ArticleRepository;
        private readonly IArticleMapper _ArticleMapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly KalabeanFileProvider _fileProvider;

        public ArticleService(IArticleRepository ArticleRepository,
                              IArticleMapper ArticleMapper,
                              IUnitOfWork unitOfWork,
                              KalabeanFileProvider fileProvider)
        {
            _ArticleRepository = ArticleRepository;
            _ArticleMapper = ArticleMapper;
            _unitOfWork = unitOfWork;
            this._fileProvider = fileProvider;
        }

        public async Task<IEnumerable<ArticleResponse>> GetArticlesAsync()
        {
            var result = await _ArticleRepository.Get();
            return result.Select(c => _ArticleMapper.Map(c));
        }
        public async Task<ArticleResponse> GetArticleAsync(GetArticleRequest request)
        {
            if (request?.Id == null) throw new ArgumentNullException();
            var Article = await _ArticleRepository.GetById(request.Id);
            return _ArticleMapper.Map(Article);
        }
        public async Task<ArticleResponse> AddArticleAsync(AddArticleRequest request)
        {
            var item = _ArticleMapper.Map(request);
            item.HasImage = request.Image != null;
            var result = _ArticleRepository.Add(item);
            if (await _unitOfWork.CommitAsync() > 0 &&
                request.Image != null)
            {
                using (var fileContent = request.Image.OpenReadStream())
                    _fileProvider.SaveArticleImage(fileContent, result.Id);

                using (var fileContent = request.File.OpenReadStream())
                    _fileProvider.SaveArticleFile(fileContent, result.Id,System.IO.Path.GetExtension(request.File.FileName));
            }
            return await this.GetArticleAsync(new GetArticleRequest { Id = result.Id });
        }
        public async Task<ArticleResponse> EditArticleAsync(EditArticleRequest request)
        {
            var existingRecord = await _ArticleRepository.GetById(request.Id);

            if (existingRecord == null)
                throw new ArgumentException($"Entity with {request.Id} is not present");

            var entity = _ArticleMapper.Map(request);
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
    }
}
