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
        IPossibilitiesShopCenterRepository _servicesRepository;
        IShoppingCenterTypeRepository _typeRepository;
        private readonly Files _filesConfig = null;
        public ShoppingCentersController(IShoppingCenterRepository repository,
            IPossibilitiesShopCenterRepository servicesRepository,
            IShoppingCenterTypeRepository typeRepository,
            IOptions<Files> filesConfig)
        {
            this._servicesRepository = servicesRepository;
            this._repository = repository;
            this._typeRepository = typeRepository;
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

            List<ShoppingCenterViewModel> shoppingCenters = null;
            if (shoppings.Count() > 0)
            {
                shoppingCenters = shoppings.Select(s => new ShoppingCenterViewModel(_filesConfig.BaseUrl)
                {
                    TypeId = typeId,
                    TypeName = typeName,
                    Id = s.Id,
                    Name = s.Name,
                    Address = s.Address,
                    Description = s.Description,
                    StoresCount = s.Stores != null ? s.Stores.Count : 0,
                    Lat = s.Lat,
                    Lng = s.Lng
                }).
                ToList();
            }
            return View(new ShoppingCentersModel(_filesConfig.BaseUrl) {
                ShoppingCenters = shoppingCenters,
                Types = _typeRepository.List(t => !t.IsDeleted).
                    Select(t => new ShoppingCenterTypeModel(_filesConfig.BaseUrl) {
                        Name = t.Name,
                        Id=t.Id
                    }).ToList()
            });
        }

        public async Task<IActionResult> ShoppingCenterDetails(string name, int id)
        {
            ShoppingCenter shopping = await this._repository.
                GetById(id);

            ShoppingCenterViewModel model = null;
            if (shopping != null)
            {
                model = new ShoppingCenterViewModel(_filesConfig.BaseUrl)
                {
                    Id = shopping.Id,
                    Name = shopping.Name,
                    Description = shopping.Description,
                    Address  = shopping.Address,
                    Tel = shopping.Tel,
                    WorkingHours = shopping.WorkingHours,
                    VirtualTourUrl = shopping.VirtualTourUrl,
                    Website = shopping.WebsiteUrl
                };

                if (!string.IsNullOrEmpty(shopping.ShoppingCenterServices))
                {
                    model.Services = new List<ShoppingCenterServiceModel>();
                    string[] ids = shopping.ShoppingCenterServices.Trim('|').Split("|");
                    foreach (string strId in ids)
                    {
                        PossibilitiesShopCenter service = await _servicesRepository.
                            GetById(int.Parse(strId));
                        if (service != null)
                        {
                            model.Services.Add(new ShoppingCenterServiceModel(_filesConfig.BaseUrl)
                            {
                                Name = service.Name,
                                Id = service.Id
                            });
                        }
                    }
                }

                if (shopping.Stores != null)
                {
                    foreach (Store store in shopping.Stores)
                    {
                        model.Stores = new List<StoreViewModel>();

                        model.Stores.Add(new StoreViewModel(_filesConfig.BaseUrl)
                        {
                            Name = store.Name,
                            Id = store.Id,
                            Category = new CategoryViewModel(_filesConfig.BaseUrl) {
                                Id = store.Category.Id,
                                Name = store.Category.Name
                            },
                            DiscountPercentage = store.DiscountPercentage
                        });
                    }
                }
            }
            return View(model);
        }
    }
}
