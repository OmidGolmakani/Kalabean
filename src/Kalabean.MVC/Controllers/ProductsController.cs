using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.City;
using Kalabean.Domain.Requests.Product;
using Kalabean.Domain.Requests.Requirement;
using Kalabean.Domain.Responses;
using Kalabean.Domain.Services;
using Kalabean.Infrastructure;
using Kalabean.Infrastructure.AppSettingConfigs;
using Kalabean.MVC.Filters;
using Kalabean.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kalabean.Controllers
{

    [Route("Product")]
    public class ProductsController : Controller
    {
        private readonly ICityService _city;
        private readonly ICategoryService _category;
        private readonly IRequirementService _requirement;
        private readonly IProductService _product;
        private readonly IOptions<Files> filesConfig;
        private readonly AppDbContext _dbContext;
        private readonly Files _filesConfig;

        public ProductsController(ICityService city,
                                  ICategoryService category,
                                  IRequirementService requirement,
                                  IProductService product,
                                  IOptions<Files> filesConfig,
                                  AppDbContext dbContext)
        {
            this._city = city;
            this._category = category;
            this._requirement = requirement;
            this._product = product;
            this.filesConfig = filesConfig;
            this._dbContext = dbContext;
            this._filesConfig = filesConfig.Value;
        }
        [HttpGet("ListProduct")]
        public async Task<IActionResult> Show(GetProductsRequest request)
        {
            var Result = await _product.GetProductsAsync(request);
            var model = new ListProductViewModel(_filesConfig.BaseUrl);
            model.Lst = (from p in Result.Items
                         select new ProductViewModel(_filesConfig.BaseUrl)
                         {
                             Description = p.Description,
                             Id = p.Id,
                             Images = p.Images,
                             Name = p.ProductName,
                             Price = p.Price,
                             Discount = p.Discount,
                             CategoryId = p.CategoryThumb == null ? 0 : p.CategoryThumb.Id,
                             TargetId = p.TargetType == null ? null : p.TargetType.Id
                         }).ToList();
            return View("ListProduct", model);
        }
        [HttpGet("Product")]
        public async Task<IActionResult> Product(long? Id)
        {
            var model = new ListProductViewModel(_filesConfig.BaseUrl);
            model.LstTargtType = (from t in _dbContext.TargetTypes
                                  select new SelectListItem()
                                  {
                                      Value = t.Id.ToString(),
                                      Text = t.Name
                                  }).ToList();
            var Categories = await _category.GetCategoriesAsync(new Domain.Requests.Category.GetCategoriesRequest());
            model.Categories = Categories.Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToList();

            if (Id != null)
            {
                var p = await _product.GetProductAsync(new GetProductRequest() { Id = (Id ?? 0) });
                model.Current = new ProductViewModel(_filesConfig.BaseUrl)
                {
                    Description = p.Description,
                    Id = p.Id,
                    Images = p.Images,
                    Name = p.ProductName,
                    Price = p.Price,
                    Discount = p.Discount,
                    CategoryId = p.CategoryThumb == null ? 0 : p.CategoryThumb.Id,
                    TargetId = p.TargetType == null ? null : p.TargetType.Id,
                    LinkProduct = p.LinkProduct
                };
            }
            return View(model);
        }
        [HttpPost("Product")]
        public async Task<IActionResult> Product(EditProductRequest request)
        {
            request.UserId = Kalabean.Infrastructure.Helpers.JWTTokenManager.GetUserIdByCookie();
            if (request.Id == 0)
            {
                await _product.AddProductAsync(new AddProductRequest()
                {
                    ArchivingDate = request.ArchivingDate,
                    Barcode = request.Barcode,
                    CategoryId = request.CategoryId,
                    CompanyName = request.CompanyName,
                    Description = request.Description,
                    Discount = request.Discount,
                    File = request.File,
                    HtmlContent = request.HtmlContent,
                    Images = request.Images,
                    IsEnabled = request.IsEnabled,
                    IsNew = request.IsNew,
                    Keywords = request.Keywords,
                    LinkProduct = request.LinkProduct,
                    Manufacturer = request.Manufacturer,
                    Model = request.Model,
                    Num = request.Num,
                    Order = request.Order,
                    Price = request.Price,
                    ProductName = request.ProductName,
                    Properties = request.Properties,
                    PublishingDate = request.PublishingDate,
                    Series = request.Series,
                    StoreId = request.StoreId,
                    TargetTypeId = request.TargetTypeId,
                    UserId = request.UserId
                });
            }
            else
            {
                await _product.EditProductAsync(request);
            }
            return RedirectToAction("Show", new GetProductsRequest());
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
                    CityId = getRequirement.CategoryId
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
                    Id = request.RequestProduct.Id
                });
            }
            return RedirectToAction("Request", "Product");
        }
        [HttpGet("ListRequest")]
        public async Task<IActionResult> ListRequest([FromQuery] Domain.Requests.Requirement.GetRequirementsRequest request)
        {
            var model = new ListRequestProductViewModel1();
            if (Infrastructure.Helpers.JWTTokenManager.GetUserIdByCookie() == 0) return RedirectToAction("Login", "User");
            var requirements = await _requirement.GetRequirementsAsync(request);
            model.Lst = (from r in requirements.Items
                         select new RequestProductViewModel()
                         {
                             CategoryId = r.CategoryId,
                             Category = r.CategoryThumb.Name,
                             City = r.CityThumb.Name,
                             CityId = r.CityId,
                             Description = r.Description,
                             Id = r.Id,
                             ProductName = r.ProductName
                         }).ToList();
            return View("ListRequest", model);
        }
    }
}
