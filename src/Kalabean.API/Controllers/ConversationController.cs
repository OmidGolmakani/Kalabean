using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.Conversation;
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
    [Route("api/conversations")]
    public class ConversationController : BaseController
    {
        private readonly IConversationService _ConversationService;
        public ConversationController(IConversationService ConversationService)
        {
            _ConversationService = ConversationService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetConversationsRequest request)
        {
            return Ok(await _ConversationService.GetConversationsAsync(request));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _ConversationService.GetConversationAsync(new GetConversationRequest { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] AddConversationRequest request)
        {
            var result = await _ConversationService.AddConversationAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
    }
}
