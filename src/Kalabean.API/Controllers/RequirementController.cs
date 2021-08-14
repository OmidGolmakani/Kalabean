﻿using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.Requirement;
using Kalabean.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Kalabean.API.Controllers
{
    public class RequirementController : BaseController
    {
        IRequirementService _RequirementService = null;
        public RequirementController(IRequirementService RequirementService)
        {
            _RequirementService = RequirementService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _RequirementService.GetRequirementsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _RequirementService.GetRequirementAsync(new GetRequirementRequest { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddRequirementRequest request)
        {
            var result = await _RequirementService.AddRequirementAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPost("BatchDelete")]
        public async Task<IActionResult> BatchDelete(long[] Ids)
        {
            await _RequirementService.BatchDeleteRequirementsAsync(Ids);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EditRequirementRequest request)
        {
            request.Id = id;
            var result = await _RequirementService.EditRequirementAsync(request);
            return Ok(result);
        }
    }
}
