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
    public class LastProductsViewComponent : ViewComponent
    {
        IProductRepository _productRepository = null;
        private readonly Files _filesConfig = null;
        public LastProductsViewComponent(IProductRepository productRepository,
            IOptions<Files> filesConfig)
        {
            _productRepository = productRepository;
            _filesConfig = filesConfig?.Value;
        }
        public async Task<IViewComponentResult> InvokeAsync(bool isDone)
        {
            List<Product> stores = _productRepository.
                List(p => p.IsEnabled && !p.IsDeleted && p.ProductImages != null).
                OrderByDescending(s => s.Id).
                Take(15).
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
                    Id = s.Id
                }).
                ToList();
            }
            return View(model);
        }
    }
}
