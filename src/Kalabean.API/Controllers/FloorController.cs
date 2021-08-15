using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.Category;
using Kalabean.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Kalabean.API.Controllers
{
    [Route("api/floors")]
    public class FloorController : BaseController
    {
        IFloorService _floorService = null;
        public FloorController(IFloorService floorService)
        {
            _floorService = floorService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _floorService.GetFloorsAsync());
        }
    }
}
