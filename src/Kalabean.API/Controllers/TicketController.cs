using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.Ticket;
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
    [Route("api/Tickets")]
    public class TicketController : BaseController
    {
        private readonly ITicketService _TicketService;
        public TicketController(ITicketService TicketService)
        {
            _TicketService = TicketService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetTicketsRequest request)
        {
            return Ok(await _TicketService.GetTicketsAsync(request));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _TicketService.GetTicketAsync(new GetTicketRequest { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] AddTicketRequest request)
        {
            var result = await _TicketService.AddTicketAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
    }
}
