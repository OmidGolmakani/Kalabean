using Kalabean.Domain.Entities;
using Kalabean.Domain.Repositories;
using Kalabean.Domain.Requests.ShoppingCenter;
using Kalabean.Domain.Responses;
using Kalabean.Domain.Services;
using Kalabean.Infrastructure.AppSettingConfigs;
using Kalabean.MVC.Helpers;
using Kalabean.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Kalabean.API.Controllers
{
    public class ShoppingCentersController : Controller
    {
        IShoppingCenterRepository _repository;
        private readonly Files _filesConfig = null;
        public ShoppingCentersController(IShoppingCenterRepository repository,
            IOptions<Files> filesConfig)
        {
            this._repository = repository;
            _filesConfig = filesConfig?.Value;
        }

        public IActionResult ShoppingCenters(string typeName, int typeId, string query, int? cityId)
        {
            IQueryable<ShoppingCenter> shoppings = this._repository.
                List(r => r.IsEnabled && !r.IsDeleted && r.Type.Id == typeId);
            if (!string.IsNullOrEmpty(query))
                shoppings = shoppings.Where(s => s.Name.Contains(query));
            if (cityId.HasValue && cityId > 0)
                shoppings = shoppings.Where(s => s.CityId == cityId);

            List<ShoppingCenterViewModel> model = null;
            if (shoppings.Count() > 0)
            {
                model = shoppings.Select(s => new ShoppingCenterViewModel(_filesConfig.BaseUrl)
                {
                    TypeId = typeId,
                    TypeName = typeName,
                    Id = s.Id,
                    Name = s.Name,
                    Address = s.Address,
                    Description = s.Description,
                    StoresCount = s.Stores != null ? s.Stores.Count : 0
                }).
                ToList();
            }
            return View(model);
        }

        public IActionResult ShoppingCenterDeatails(string name, int id)
        {
            ShoppingCenter shoppings = this._repository.
                GetById(id).Result;


            ShoppingCenterDeatailViewModel model = null;
            if (shoppings != null)
            {
                model = new ShoppingCenterDeatailViewModel(_filesConfig.BaseUrl)
                {
                    ShoppingCenterDeatails = shoppings,
                    Longitude = "36.32356134189473",
                    Latitude = "59.57780599594117",
                    Services = shoppings.ShoppingCenterServices.Trim().Split().ToList()
                };
                if (model.ShoppingCenterDeatails?.Stores != null)
                {
                    foreach (var item in model.ShoppingCenterDeatails.Stores)
                    {
                        var store = new StoreViewModel(_filesConfig.BaseUrl);
                        store.Name = item.Name;
                        store.Category.Name = item.Category.Name;
                        store.Id = item.Id;

                        model.StoreModels.Add(store);
                    }
                }
            }
            return View(model);
        }
    }
}
