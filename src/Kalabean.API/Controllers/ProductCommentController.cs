using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.ProductComment;
using Kalabean.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Kalabean.API.Controllers
{
    [Route("api/productcomment")]
    public class ProductCommentController : BaseController
    {
        IProductCommentService _productCommentService = null;
        public ProductCommentController(IProductCommentService productCommentService)
        {
            _productCommentService = productCommentService;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetProductCommentsRequest request)
        {
            return Ok(await _productCommentService.GetProductCommentsAsync(request));
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            return Ok(await _productCommentService.GetProductCommentAsync(new GetProductCommentRequest { Id = id }));
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] AddProductCommentRequest request)
        {
            var result = await _productCommentService.AddProductCommentAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPost("BatchDelete")]
        public async Task<IActionResult> BatchDelete(long[] Ids)
        {
            await _productCommentService.BatchDeleteProductCommentsAsync(Ids);
            return Ok();
        }
        [AllowAnonymous]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] EditProductCommentRequest request)
        {
            request.Id = id;
            var result = await _productCommentService.EditProductCommentAsync(request);
            return Ok(result);
        }
        [HttpPost("UpdateCommentStatus")]
        public async Task<IActionResult> UpdateCommentStatus(EditProductCommnetStatusRequest request)
        {
            await _productCommentService.UpdateProductCommentStatusAsync(request);
            return Ok();
        }
    }
}
