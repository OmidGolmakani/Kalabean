using System.Linq;
using Kalabean.Domain.Mappers;
using Kalabean.Domain.Repositories;
using Kalabean.Domain.Requests.RolePermission;
using Kalabean.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Kalabean.Domain.Base;
using Kalabean.Domain.Services;

namespace Kalabean.Infrastructure.Services
{
    public class RolePermissionService : IRolePermissionService
    {
        private readonly IRolePermissionRepository _RolePermissionRepository;
        private readonly IRolePermissionMapper _RolePermissionMapper;
        private readonly IUnitOfWork _unitOfWork;
        public RolePermissionService(IRolePermissionRepository RolePermissionRepository,
                                     IRolePermissionMapper RolePermissionMapper,
                                     IUnitOfWork unitOfWork)
        {
            _RolePermissionRepository = RolePermissionRepository;
            _RolePermissionMapper = RolePermissionMapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<RolePermissionResponse>> GetRolePermissionsAsync(long RoleId)
        {
            var result = await _RolePermissionRepository.Get(RoleId);
            return result.Select(p => _RolePermissionMapper.Map(p));
        }
        public async Task<RolePermissionResponse> GetRolePermissionAsync(GetRolePermissionRequest request)
        {
            if (request?.Id == null) throw new ArgumentNullException();
            var RolePermission = await _RolePermissionRepository.GetById(request.Id);
            return _RolePermissionMapper.Map(RolePermission);
        }
        public async Task<RolePermissionResponse> AddRolePermissionAsync(AddRolePermissionRequest request)
        {
            var item = _RolePermissionMapper.Map(request);
            item.Token = Helpers.JWTTokenManager.GeneratePermissionToken(item);
            var result = _RolePermissionRepository.Add(item);
            await _unitOfWork.CommitAsync();

            return _RolePermissionMapper.Map(await _RolePermissionRepository.GetById(result.Id));
        }
        public async Task<RolePermissionResponse> EditRolePermissionAsync(EditRolePermissionRequest request)
        {
            var existingRecord = await _RolePermissionRepository.GetById(request.Id);

            if (existingRecord == null)
                throw new ArgumentException($"Entity with {request.Id} is not present");

            var entity = _RolePermissionMapper.Map(request);
            entity.Token = Helpers.JWTTokenManager.GeneratePermissionToken(entity);
            var result = _RolePermissionRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            return _RolePermissionMapper.Map(await _RolePermissionRepository.GetById(result.Id));
        }

        public async Task BatchDeleteRolePermissionsAsync(long[] ids)
        {
            List<Kalabean.Domain.Entities.RolePermission> RolePermissions =
                _RolePermissionRepository.List(c => ids.Contains(c.Id)).ToList();
            foreach (Kalabean.Domain.Entities.RolePermission RolePermission in RolePermissions)
                RolePermission.IsDeleted = true;
            _RolePermissionRepository.UpdateBatch(RolePermissions);

            await _unitOfWork.CommitAsync();
        }
    }
}
