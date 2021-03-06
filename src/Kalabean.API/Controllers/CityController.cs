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
    [Route("api/cities")]
    public class CityController : BaseController
    {
        private readonly ICityService _cityService;
        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _cityService.GetCitiesAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _cityService.GetCityAsync(new GetCityRequest { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] AddCityRequest request)
        {
            var result = await _cityService.AddCityAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPost("BatchDelete")]
        public async Task<IActionResult> BatchDelete(int[] Ids)
        {
            await _cityService.BatchDeleteCitiesAsync(Ids);
            return Ok();
        }

        [HttpPut("{id}")]
        [Microsoft.AspNetCore.Cors.EnableCors("Kalabean")]
        public async Task<IActionResult> Put(int id, [FromForm] EditCityRequest request)
        {
            request.Id = id;
            var result = await _cityService.EditCityAsync(request);
            return Ok(result);
        }
    }
}
