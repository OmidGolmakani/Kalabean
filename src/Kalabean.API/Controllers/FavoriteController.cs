using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.Favorites;
using Kalabean.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Kalabean.API.Controllers
{
    [Route("api/Favorite")]
    public class FavoriteController : BaseController
    {
        IFavoriteService _favoriteService = null;
        public FavoriteController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetFavoritesRequest request)
        {
            return Ok(await _favoriteService.GetFavoritesAsync(request));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _favoriteService.GetFavoriteAsync(new GetFavoriteRequest { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] AddFavoriteRequest request)
        {
            var result = await _favoriteService.AddFavoriteAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPost("BatchDelete")]
        public async Task<IActionResult> BatchDelete(long[] Ids)
        {
            await _favoriteService.BatchDeleteFavoritesAsync(Ids);
            return Ok();
        }
    }
}
