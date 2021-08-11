using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.User;
using Kalabean.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Kalabean.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        IUserService _userService = null;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userService.GetUsersAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _userService.GetUserAsync(new GetUserRequest { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddUserRequest request)
        {
            var result = await _userService.AddUserAsync(request);
            if (result != null)
            {
                await _userService.AddUserToRole(new AddUserToRoleRequest() { Id = result.Id, Roles = new List<string>() { "User" } });
            }
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        [HttpPost("Signin")]
        public async Task<IActionResult> Signin(LoginRequest request)
        {
            return Ok(await _userService.SignIn(request));
        }
        [HttpPost("Signout")]
        public async Task<IActionResult> Signout()
        {
            await _userService.SignOut();
            return Ok("");
        }
        [HttpPost("AddUserToRole")]
        public async Task<IActionResult> AddUserToRole(AddUserToRoleRequest request)
        {
            return Ok(await _userService.AddUserToRole(request));
        }
    }
}
