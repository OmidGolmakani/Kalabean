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
    public class OrderController : BaseController
    {
        private readonly IOrderService _OrderService;
        public OrderController(IOrderService OrderService)
        {
            _OrderService = OrderService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _OrderService.GetOrdersAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
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
        public async Task<IActionResult> Put(int id,EditOrderHeaderRequest request)
        {
            request.Id = id;
            var result = await _OrderService.EditOrderAsync(request);
            return Ok(result);
        }
    }
}
