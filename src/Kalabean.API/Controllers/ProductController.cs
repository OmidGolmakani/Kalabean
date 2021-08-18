using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.Product;
using Kalabean.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Kalabean.API.Controllers
{
    [Route("api/products")]
    public class ProductController : BaseController
    {
        IProductService _productService = null;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetProductsRequest request)
        {
            return Ok(await _productService.GetProductsAsync(request));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _productService.GetProductAsync(new GetProductRequest { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] AddProductRequest request)
        {
            var result = await _productService.AddProductAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPost("BatchDelete")]
        public async Task<IActionResult> BatchDelete(long[] Ids)
        {
            await _productService.BatchDeleteProductsAsync(Ids);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] EditProductRequest request)
        {
            request.Id = id;
            var result = await _productService.EditProductAsync(request);
            return Ok(result);
        }
    }
}
