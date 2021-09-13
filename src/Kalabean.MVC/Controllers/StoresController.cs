
using Kalabean.Domain.Entities;
using Kalabean.Domain.Repositories;
using Kalabean.Infrastructure.AppSettingConfigs;
using Kalabean.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kalabean.MVC.Controllers
{
    [Route("StoreProfile")]

    public class StoresController : Controller
    {
        IStoreRepository _storeRepository;
        IProductRepository _productRepository;
        private readonly Files _filesConfig = null;
        public StoresController(IStoreRepository storeRepository,
            IProductRepository productRepository,
            IOptions<Files> filesConfig)
        {
            _productRepository = productRepository;
            _storeRepository = storeRepository;
            _filesConfig = filesConfig?.Value;
        }
        public async Task<IActionResult> StoreProfile(string name, int id)
        {
            Store store = await this._storeRepository.GetById(id);
            var model = new StoreViewModel(_filesConfig.BaseUrl);
            if (store == null)
            {
                return View(model);
            }
            return View(new StoreViewModel(_filesConfig.BaseUrl) {
                Id = store.Id,
                Name = store.Name,
                Category = new CategoryViewModel(_filesConfig.BaseUrl) {
                    Id = store.Category.Id,
                    Name = store.Category.Name
                },
                Products = _productRepository.
                    List(p => !p.IsDeleted && p.IsEnabled && p.StoreId == id).
                    Select(p => new ProductViewModel(_filesConfig.BaseUrl) {
                    Id = p.Id,
                    Name = p.ProductName,
                    Price = p.Price,
                    FormattedPrice = string.Format("{0:n0}", p.Price)
                    }).ToList(),
                Address = store.Address,
                CityName = store.ShoppingCenter?.City?.Name,
                Tel = store.Tel,
                VirtualTour = store.VirtualTourUrl,
                WorkingHours = store.WorkingHours,
                Description = store.Description,
                Instagram = store.InstagramUrl,
                Telegram = store.TelegramUrl,
                
            });
        }
    }
}
