using System.Linq;
using Kalabean.Domain.Mappers;
using Kalabean.Domain.Repositories;
using Kalabean.Domain.Requests.City;
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
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly ICityMapper _cityMapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly KalabeanFileProvider _fileProvider;
        public CityService(ICityRepository cityRepository,
            ICityMapper cityMapper,
            IUnitOfWork unitOfWork,
            IFileAccessProvider fileProvider)
        {
            _cityRepository = cityRepository;
            _cityMapper = cityMapper;
            _unitOfWork = unitOfWork;
            _fileProvider = new KalabeanFileProvider(fileProvider);
        }

        public async Task<IEnumerable<CityResponse>> GetCitiesAsync()
        {
            var result = _cityRepository.List(c => !c.IsDeleted)
                .Include(c => c.AccessRule);
            return result.Select(c => _cityMapper.Map(c));
        }
        public async Task<CityResponse> GetCityAsync(GetCityRequest request)
        {
            if (request?.Id == null) throw new ArgumentNullException();
            var city = await _cityRepository.GetById(request.Id);
            return _cityMapper.Map(city);
        }
        public async Task<CityResponse> AddCityAsync(AddCityRequest request)
        {
            var item = _cityMapper.Map(request);
            item.HasImage = request.Image != null;
            var result = _cityRepository.Add(item);
            if (await _unitOfWork.CommitAsync() > 0 &&
                request.Image != null)
            {
                using (var fileContent = request.Image.OpenReadStream())
                    _fileProvider.SaveCityImage(fileContent, result.Id);
            }
            return await this.GetCityAsync(new GetCityRequest { Id = result.Id });
        }
        public async Task<CityResponse> EditCityAsync(EditCityRequest request)
        {
            var existingRecord = await _cityRepository.GetById(request.Id);

            if (existingRecord == null)
                throw new ArgumentException($"Entity with {request.Id} is not present");

            var entity = _cityMapper.Map(request);
            if (entity.HasImage || request.Image != null) {
                if (request.ImageEdited) {
                    if (request.Image != null)
                    {
                        using (var fileContent = request.Image.OpenReadStream())
                            _fileProvider.SaveCityImage(fileContent, entity.Id);
                        entity.HasImage = true;
                    }
                    else {
                        _fileProvider.DeleteCityImage(entity.Id);
                        entity.HasImage = false;
                    }
                }
            }
            var result = _cityRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            return await this.GetCityAsync(new GetCityRequest { Id = result.Id });
        }

        public async Task BatchDeleteCitiesAsync(int[] ids)
        {
            List<Kalabean.Domain.Entities.City> cities =
                _cityRepository.List(c => ids.Contains(c.Id)).ToList();
            foreach (Kalabean.Domain.Entities.City city in cities)
                city.IsDeleted = true;
            _cityRepository.UpdateBatch(cities);

            await _unitOfWork.CommitAsync();
        }
    }
}
