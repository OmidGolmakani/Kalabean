﻿using System.Linq;
using Kalabean.Domain.Mappers;
using Kalabean.Domain.Repositories;
using Kalabean.Domain.Requests.Store;
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
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IStoreMapper _storeMapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly KalabeanFileProvider _fileProvider;
        public StoreService(IStoreRepository storeRepository,
            IStoreMapper storeMapper,
            IUnitOfWork unitOfWork,
            IFileAccessProvider fileProvider)
        {
            _storeMapper = storeMapper;
            _storeRepository = storeRepository;
            _unitOfWork = unitOfWork;
            _fileProvider = new KalabeanFileProvider(fileProvider);
        }

        public async Task<IEnumerable<StoreResponse>> GetStoresAsync()
        {
            var result = await _storeRepository.Get();
            return result.Select(c => _storeMapper.Map(c));
        }
        public async Task<StoreResponse> GetStoreAsync(GetStoreRequest request)
        {
            if (request?.Id == null) throw new ArgumentNullException();
            return _storeMapper.Map(await _storeRepository.GetById(request.Id));
        }
        public async Task<StoreResponse> AddStoreAsync(AddStoreRequest request)
        {
            var item = _storeMapper.Map(request);
            item.HasImage = request.Image != null;
            var result = _storeRepository.Add(item);
            if (await _unitOfWork.CommitAsync() > 0 &&
                request.Image != null)
            {
                using (var fileContent = request.Image.OpenReadStream())
                    _fileProvider.SaveStoreImage(fileContent, result.Id);
            }
            return _storeMapper.Map(await _storeRepository.GetById(result.Id));
        }
        public async Task<StoreResponse> EditStoreAsync(EditStoreRequest request)
        {
            var existingRecord = await _storeRepository.GetById(request.Id);

            if (existingRecord == null)
                throw new ArgumentException($"Entity with {request.Id} is not present");

            var entity = _storeMapper.Map(request);
            if (entity.HasImage || request.Image != null)
            {
                if (request.ImageEdited)
                {
                    if (request.Image != null)
                    {
                        using (var fileContent = request.Image.OpenReadStream())
                            _fileProvider.SaveStoreImage(fileContent, entity.Id);
                        entity.HasImage = true;
                    }
                    else
                    {
                        _fileProvider.DeleteStoreImage(entity.Id);
                        entity.HasImage = false;
                    }
                }
            }
            var result = _storeRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            return _storeMapper.Map(await _storeRepository.GetById(result.Id));
        }

        public async Task BatchDeleteStoresAsync(int[] ids)
        {
            List<Kalabean.Domain.Entities.Store> shoppings =
                _storeRepository.List(c => ids.Contains(c.Id)).ToList();
            foreach (Kalabean.Domain.Entities.Store shopping in shoppings)
                shopping.IsDeleted = true;
            _storeRepository.UpdateBatch(shoppings);

            await _unitOfWork.CommitAsync();
        }
    }
}
