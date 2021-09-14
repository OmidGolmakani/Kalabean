using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kalabean.MVC.Controllers
{
    public class ProductsController : Controller
    {
        public async Task<IActionResult> Product(Domain.Requests.Product.GetProductRequest request)
        {
            return View();
        }
        public async Task<IActionResult> UpdateProduct(Domain.Requests.Product.EditProductRequest request)
        {
            return View();
        }
    }
}
