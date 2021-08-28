using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.OrderHeader;
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
    [Route("api/orders")]
    public class OrderController : BaseController
    {
        private readonly IOrderService _OrderService;
        public OrderController(IOrderService OrderService)
        {
            _OrderService = OrderService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetOrdersRequest request)
        {
            return Ok(await _OrderService.GetOrdersAsync(request));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(await _OrderService.GetOrderAsync(new GetOrderHeaderRequest { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] AddOrderHeaderRequest request)
        {
            var result = await _OrderService.AddOrderAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPost("BatchDelete")]
        public async Task<IActionResult> BatchDelete(long[] Ids)
        {
            await _OrderService.BatchDeleteOrdersAsync(Ids);
            return Ok();
        }

        [HttpPut("{id}")]
        [Microsoft.AspNetCore.Cors.EnableCors("Kalabean")]
        public async Task<IActionResult> Put(long id, [FromForm] EditOrderHeaderRequest request)
        {
            request.Id = id;
            var result = await _OrderService.EditOrderAsync(request);
            return Ok(result);
        }
        [HttpPut("Publishe{id}")]
        [Microsoft.AspNetCore.Cors.EnableCors("Kalabean")]
        public async Task<IActionResult> Publish(long id)
        {
            await _OrderService.PublishOrderAsync(id);
            return Ok(_OrderService.GetOrderAsync(new GetOrderHeaderRequest() { Id = id }));
        }
    }
}
