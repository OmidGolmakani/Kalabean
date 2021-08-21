using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.Advertise;
using Kalabean.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Kalabean.API.Controllers
{
    [Route("api/advertiseing")]
    public class AdvertiseController : BaseController
    {
        IAdvertiseService _AdvertiseService = null;
        public AdvertiseController(IAdvertiseService AdvertiseService)
        {
            _AdvertiseService = AdvertiseService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAdvertisingRequest request)
        {
            return Ok(await _AdvertiseService.GetAdvertisingAsync(request));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _AdvertiseService.GetAdvertiseAsync(new GetAdvertiseRequest { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] AddAdvertiseRequest request)
        {
            var result = await _AdvertiseService.AddAdvertiseAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPost("BatchDelete")]
        public async Task<IActionResult> BatchDelete(int[] Ids)
        {
            await _AdvertiseService.BatchDeleteAdvertisingAsync(Ids);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] EditAdvertiseRequest request)
        {
            request.Id = id;
            var result = await _AdvertiseService.EditAdvertiseAsync(request);
            return Ok(result);
        }
    }
}
