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
    public class HomeCategoriesViewComponent : ViewComponent
    {
        ICategoryRepository _repository = null;
        private readonly Files _filesConfig = null;
        public HomeCategoriesViewComponent(ICategoryRepository repository,
            IOptions<Files> filesConfig)
        {
            _repository = repository;
            _filesConfig = filesConfig?.Value;
        }
        public async Task<IViewComponentResult> InvokeAsync(bool isDone)
        {
            List<Category> categories = _repository.
                List(p => !p.IsDeleted).
                OrderBy(s => s.Order).
                Include(s => s.Products).
                Include(s => s.Stores).
                Take(15).
                ToList();
            List<CategoryViewModel> model = null;
            if (categories.Count > 0)
            {
                model = categories.Select(s => new CategoryViewModel(_filesConfig.BaseUrl)
                {
                    Name = s.Name,
                    Id = s.Id,
                    StoresCount = s.Stores != null ? s.Stores.Count : 0,
                    ProductsCount = s.Products != null ? s.Products.Count : 0

                }).
                ToList();
            }
            return View(model);
        }
    }
}
