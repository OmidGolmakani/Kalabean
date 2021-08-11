using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.RolePermission;
using Kalabean.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Kalabean.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolePermissionController : ControllerBase
    {
        IRolePermissionService _rolePermissionService = null;
        public RolePermissionController(IRolePermissionService rolePermissionService)
        {
            _rolePermissionService = rolePermissionService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(long RoleId)
        {
            return Ok(await _rolePermissionService.GetRolePermissionsAsync(RoleId));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _rolePermissionService.GetRolePermissionAsync(new GetRolePermissionRequest { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddRolePermissionRequest request)
        {
            var result = await _rolePermissionService.AddRolePermissionAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPost("BatchDelete")]
        public async Task<IActionResult> BatchDelete(long[] Ids)
        {
            await _rolePermissionService.BatchDeleteRolePermissionsAsync(Ids);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EditRolePermissionRequest request)
        {
            request.Id = id;
            var result = await _rolePermissionService.EditRolePermissionAsync(request);
            return Ok(result);
        }
    }
}
