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

namespace Kalabean.Infrastructure.Services
{
    public class ShoppingCenterTypeService : IShoppingCenterTypeService
    {
        private readonly IShoppingCenterTypeRepository _typeRepository;
        private readonly IShoppingCenterTypeMapper _typeMapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly KalabeanFileProvider _fileProvider;
        public ShoppingCenterTypeService(IShoppingCenterTypeRepository typeRepository,
            IShoppingCenterTypeMapper typeMapper,
            IUnitOfWork unitOfWork,
            IFileAccessProvider fileProvider)
        {
            _typeRepository = typeRepository;
            _typeMapper = typeMapper;
            _unitOfWork = unitOfWork;
            _fileProvider = new KalabeanFileProvider(fileProvider);
        }

        public async Task<IEnumerable<ShoppingCenterTypeResponse>> GetTypesAsync()
        {
            var result = _typeRepository.List(c => !c.IsDeleted)
                .Include(c => c.AccessRule);
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
            if (await _unitOfWork.CommitAsync() > 0 &&
                request.Image != null)
            {
                using (var fileContent = request.Image.OpenReadStream())
                    _fileProvider.SaveTypeImage(fileContent, result.Id);
            }
            return _typeMapper.Map(await _typeRepository.GetById(result.Id));
        }
        public async Task<ShoppingCenterTypeResponse> EditTypeAsync(EditShoppingCenterTypeRequest request)
        {
            var existingRecord = await _typeRepository.GetById(request.Id);

            if (existingRecord == null)
                throw new ArgumentException($"Entity with {request.Id} is not present");

            var entity = _typeMapper.Map(request);
            if (entity.HasImage || request.Image != null) {
                if (request.ImageEdited) {
                    if (request.Image != null)
                    {
                        using (var fileContent = request.Image.OpenReadStream())
                            _fileProvider.SaveTypeImage(fileContent, entity.Id);
                        entity.HasImage = true;
                    }
                    else {
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
