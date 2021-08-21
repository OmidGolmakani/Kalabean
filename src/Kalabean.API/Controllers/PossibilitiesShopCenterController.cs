using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.PossibilitiesShopCenter;
using Kalabean.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Kalabean.API.Controllers
{
    [Route("api/PossibilitiesShopCenters")]
    public class PossibilitiesShopCenterController : BaseController
    {
        IPossibilitiesShopCenterService _PossibilitiesShopCenterService = null;
        public PossibilitiesShopCenterController(IPossibilitiesShopCenterService PossibilitiesShopCenterService)
        {
            _PossibilitiesShopCenterService = PossibilitiesShopCenterService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetPossibilitiesShopCentersRequest request)
        {
            return Ok(await _PossibilitiesShopCenterService.GetPossibilitiesShopCentersAsync(request));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _PossibilitiesShopCenterService.GetPossibilitiesShopCenterAsync(new GetPossibilitiesShopCenterRequest { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] AddPossibilitiesShopCenterRequest request)
        {
            var result = await _PossibilitiesShopCenterService.AddPossibilitiesShopCenterAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPost("BatchDelete")]
        public async Task<IActionResult> BatchDelete(int[] Ids)
        {
            await _PossibilitiesShopCenterService.BatchDeletePossibilitiesShopCentersAsync(Ids);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] EditPossibilitiesShopCenterRequest request)
        {
            request.Id = id;
            var result = await _PossibilitiesShopCenterService.EditPossibilitiesShopCenterAsync(request);
            return Ok(result);
        }
    }
}
