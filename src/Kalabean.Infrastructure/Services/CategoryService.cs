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

namespace Kalabean.Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryMapper _categoryMapper;
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(ICategoryRepository categoryRepository,
            ICategoryMapper categoryMapper,
            IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _categoryMapper = categoryMapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CategoryResponse>> GetCategoriesAsync()
        {
            var result = await _categoryRepository.Get();
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
            await _unitOfWork.CommitAsync();

            return _categoryMapper.Map(await _categoryRepository.GetById(result.Id));
        }
        public async Task<CategoryResponse> EditCategoryAsync(EditCategoryRequest request)
        {
            var existingRecord = await _categoryRepository.GetById(request.Id);

            if (existingRecord == null)
                throw new ArgumentException($"Entity with {request.Id} is not present");

            var entity = _categoryMapper.Map(request);
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
