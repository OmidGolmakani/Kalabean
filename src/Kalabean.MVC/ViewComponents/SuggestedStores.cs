using Kalabean.Domain.Entities;
using Kalabean.Domain.Repositories;
using Kalabean.Infrastructure.AppSettingConfigs;
using Kalabean.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kalabean.MVC.ViewComponents
{
    public class SuggestedStoresViewComponent : ViewComponent
    {
        IStoreRepository _storeRepository = null;
        private  readonly Files _filesConfig = null;
        public SuggestedStoresViewComponent(IStoreRepository storeRepository, 
            IOptions<Files> filesConfig)
        {
            _storeRepository = storeRepository;
            _filesConfig = filesConfig?.Value;
        }
        public async Task<IViewComponentResult> InvokeAsync(bool isDone)
        {
            List<Store> stores = _storeRepository.
                List(s => s.IsEnabled && !s.IsDeleted && s.HasImage && s.IsFeatured).
                OrderByDescending(s => s.Id).
                Take(15).
                Include(s => s.Category).
                Include(s => s.Products).
                ToList();
            List<StoreViewModel> model = null;
            if (stores.Count > 0)
            {
                model = stores.Select(s => new StoreViewModel(_filesConfig.BaseUrl)
                {
                    Category = new CategoryViewModel(_filesConfig.BaseUrl)
                    {
                        Id = s.Category.Id,
                        Name = s.Category.Name
                    },
                    Name = s.Name,
                    Id = s.Id,
                    ProductsCount = s.Products != null ? s.Products.Count : 0
                }).
                ToList();
            }
            return View(model);
        }
    }
}
