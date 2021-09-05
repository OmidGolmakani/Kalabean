using Kalabean.Domain.Entities;
using Kalabean.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kalabean.MVC.ViewComponents
{
    public class HomeTopRightViewComponent : ViewComponent
    {
        IAdvertiseRepository _repository;
        public HomeTopRightViewComponent(IAdvertiseRepository repository)
        {
            _repository = repository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            IQueryable<Advertise> ads = _repository.
                List(a => !a.IsDeleted && a.AdPositionId == 1);
            return View(ads);
        }
    }
}
