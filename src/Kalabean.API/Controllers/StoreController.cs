using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.Store;
using Kalabean.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Kalabean.API.Controllers
{
    [Route("api/stores")]
    [ApiController]
    public class StoreController : BaseController
    {
        IStoreService _storeService = null;
        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _storeService.GetStoresAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _storeService.GetStoreAsync(new GetStoreRequest  { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] AddStoreRequest request)
        {
            var result = await _storeService.AddStoreAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPost("BatchDelete")]
        public async Task<IActionResult> BatchDelete(int[] Ids)
        {
            await _storeService.BatchDeleteStoresAsync(Ids);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] EditStoreRequest request)
        {
            request.Id = id;
            var result = await _storeService.EditStoreAsync(request);
            return Ok(result);
        }
    }
}
