using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.City;
using Kalabean.Domain.Requests.Requirement;
using Kalabean.Domain.Responses;
using Kalabean.Domain.Services;
using Kalabean.MVC.Filters;
using Kalabean.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kalabean.Controllers
{

    [Route("Product")]
    [ComprleteUserProfileFilter]
    public class ProductsController : Controller
    {
        private readonly ICityService _city;
        private readonly ICategoryService _category;
        private readonly IRequirementService _requirement;

        public ProductsController(ICityService city,
                                  ICategoryService category,
                                  IRequirementService requirement)
        {
            this._city = city;
            this._category = category;
            this._requirement = requirement;
        }

        public async Task<IActionResult> Show(string name, int id)
        {
            return View();
        }
        [HttpGet("Request")]
        public async Task<IActionResult> Request(long? Id)
        {
            var model = new ListRequestProductViewModel();
            model.Cities = await _city.GetCitiesAsync();
            model.Categories = await _category.GetCategoriesAsync(new Domain.Requests.Category.GetCategoriesRequest());
            if (Id != null && Id != 0)
            {
                var getRequirement = await _requirement.GetRequirementAsync(new GetRequirementRequest() { Id = (Id ?? 0) });
                if (getRequirement == null)
                {
                    ModelState.AddModelError(string.Empty, "درخواست مورد نظر یافت نشد");
                    return View(model);
                }
                model.RequestProduct = new RequestProductViewModel()
                {
                    CategoryId = getRequirement.CategoryId,
                    CityId =getRequirement.CategoryId
                };
            }
            return View(model);
        }
        [HttpPost("Request")]
        public async Task<IActionResult> Request(ListRequestProductViewModel request)
        {
            request.RequestProduct.TypePricing = RequirementTypePriceing.Agreed;
            request.RequestProduct.Price = 0;
            if (request.RequestProduct.Id == 0)
            {
                await _requirement.AddRequirementAsync(new AddRequirementRequest()
                {
                    CategoryId = request.RequestProduct.CategoryId,
                    Description = request.RequestProduct.Description,
                    Image = request.RequestProduct.Image,
                    Price = request.RequestProduct.Price,
                    ProductName = request.RequestProduct.ProductName,
                    TypePricing = request.RequestProduct.TypePricing,
                    CityId = request.RequestProduct.CityId
                });
            }
            else
            {
                await _requirement.EditRequirementAsync(new EditRequirementRequest()
                {
                    CategoryId = request.RequestProduct.CategoryId,
                    Description = request.RequestProduct.Description,
                    Image = request.RequestProduct.Image,
                    Price = request.RequestProduct.Price,
                    ProductName = request.RequestProduct.ProductName,
                    TypePricing = request.RequestProduct.TypePricing,
                    CityId = request.RequestProduct.CityId,
                    Id =request.RequestProduct.Id
                });
            }
            return RedirectToAction("Request", "Product");
        }
    }
}
