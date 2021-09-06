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
            List<Product> products = _productRepository.
                List(p => p.IsEnabled && !p.IsDeleted && p.ProductImages != null).
                OrderByDescending(s => s.Id).
                Include(s => s.Category).
                Include(s => s.ProductImages).
                Take(15).
                ToList();
            List<ProductViewModel> model = null;
            if (products.Count > 0)
            {
                model = products.Select(s => new ProductViewModel(_filesConfig.BaseUrl)
                {
                    Name = s.ProductName,
                    Id = s.Id,
                    ImageId = s.ProductImages != null && s.ProductImages.Count > 0 ?
                     s.ProductImages?.ToList()?[0]?.Id : null
                    
                }).
                ToList();
            }
            return View(model);
        }
    }
}
