using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.City;
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
    [Route("api/rules")]
    [ApiController]
    public class RulesController : ControllerBase
    {
        public RulesController()
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok("");
        }
    }
}
