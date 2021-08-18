﻿using System.Linq;
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

namespace Kalabean.Infrastructure.Services
{
    public class RequirementService : IRequirementService
    {
        private readonly IRequirementRepository _RequirementRepository;
        private readonly IRequirementMapper _RequirementMapper;
        private readonly IUnitOfWork _unitOfWork;
        public RequirementService(IRequirementRepository RequirementRepository,
                                  IRequirementMapper RequirementMapper,
                                   IUnitOfWork unitOfWork)
        {
            _RequirementRepository = RequirementRepository;
            _RequirementMapper = RequirementMapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ListPagingResponse<RequirementResponse>> GetRequirementsAsync(GetRequirementsRequest request)
        {
            var result = await _RequirementRepository.Get(request);
            var list = result.Select(p => _RequirementMapper.Map(p));
            var count = await _RequirementRepository.Count(request);
            return new ListPagingResponse<RequirementResponse>() { Items = list, Total = count };
        }
        public async Task<RequirementResponse> GetRequirementAsync(GetRequirementRequest request)
        {
            if (request?.Id == null) throw new ArgumentNullException();
            var Requirement = await _RequirementRepository.GetById(request.Id);
            return _RequirementMapper.Map(Requirement);
        }
        public async Task<RequirementResponse> AddRequirementAsync(AddRequirementRequest request)
        {
            var item = _RequirementMapper.Map(request);
            item.UserId = Helpers.JWTTokenManager.GetUserIdByToken();
            item.RequirementStatus = (byte)RequirementStatus.AwaitingApproval;
            var result = _RequirementRepository.Add(item);
            await _unitOfWork.CommitAsync();

            return _RequirementMapper.Map(await _RequirementRepository.GetById(result.Id));
        }
        public async Task<RequirementResponse> EditRequirementAsync(EditRequirementRequest request)
        {
            var existingRecord = await _RequirementRepository.GetById(request.Id);

            if (existingRecord == null)
                throw new ArgumentException($"Entity with {request.Id} is not present");

            var entity = _RequirementMapper.Map(request);
            entity.UserId = Helpers.JWTTokenManager.GetUserIdByToken();
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
    }
}
