using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.Article;
using Kalabean.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Kalabean.API.Controllers
{
    [Route("api/articles")]
    public class ArticleController : BaseController
    {
        IArticleService _ArticleService = null;
        public ArticleController(IArticleService ArticleService)
        {
            _ArticleService = ArticleService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _ArticleService.GetArticlesAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _ArticleService.GetArticleAsync(new GetArticleRequest { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddArticleRequest request)
        {
            var result = await _ArticleService.AddArticleAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPost("BatchDelete")]
        public async Task<IActionResult> BatchDelete(long[] Ids)
        {
            await _ArticleService.BatchDeleteArticlesAsync(Ids);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EditArticleRequest request)
        {
            request.Id = id;
            var result = await _ArticleService.EditArticleAsync(request);
            return Ok(result);
        }
    }
}
