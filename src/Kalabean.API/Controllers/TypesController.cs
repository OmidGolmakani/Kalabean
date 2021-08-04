using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.ShoppingCenter;
using Kalabean.Domain.Responses;
using Kalabean.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Kalabean.API.Controllers
{
    [Route("api/types")]
    [ApiController]
    public class TypesController : ControllerBase
    {
        private readonly IShoppingCenterTypeService _typeService;
        public TypesController(IShoppingCenterTypeService typeService)
        {
            _typeService = typeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _typeService.GetTypesAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _typeService.GetTypeAsync(new GetShoppingCenterTypeRequest { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] AddShoppingCenterTypeRequest request)
        {
            var result = await _typeService.AddTypeAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPost("BatchDelete")]
        public async Task<IActionResult> BatchDelete(int[] Ids)
        {
            await _typeService.BatchDeleteTypesAsync(Ids);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] EditShoppingCenterTypeRequest request)
        {
            request.Id = id;
            var result = await _typeService.EditTypeAsync(request);
            return Ok(result);
        }
    }
}
