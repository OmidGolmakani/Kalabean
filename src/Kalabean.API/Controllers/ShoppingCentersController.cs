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
    public class ShoppingCentersController : BaseController
    {
        private readonly IShoppingCenterService _shoppingCenterService;
        public ShoppingCentersController(IShoppingCenterService shoppingCenterService)
        {
            _shoppingCenterService = shoppingCenterService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _shoppingCenterService.GetShoppingCentersAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _shoppingCenterService.
                GetShoppingCenterAsync(new GetShoppingCenterRequest { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] AddShoppingCenterRequest request)
        {
            var result = await _shoppingCenterService.AddShoppingCenterAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPost("BatchDelete")]
        public async Task<IActionResult> BatchDelete(int[] Ids)
        {
            await _shoppingCenterService.BatchDeleteShoppingCentersAsync(Ids);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] EditShoppingCenterRequest request)
        {
            request.Id = id;
            var result = await _shoppingCenterService.EditShoppingCenterAsync(request);
            return Ok(result);
        }
    }
}
