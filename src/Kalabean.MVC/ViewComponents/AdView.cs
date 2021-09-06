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

namespace Kalabean.MVC.ViewComponents
{
    public class AdViewViewComponent : ViewComponent
    {
        IAdvertiseRepository _repository;
        private readonly Files _filesConfig = null;
        public AdViewViewComponent(IAdvertiseRepository repository,
            IOptions<Files> filesConfig)
        {
            _repository = repository;
            _filesConfig = filesConfig?.Value;
        }
        public async Task<IViewComponentResult> InvokeAsync(int positionId)
        {
            List<Advertise> ads = _repository.
                List(a => !a.IsDeleted && a.AdPositionId == positionId).
                ToList();
            List<AdViewModel> model = null;
            //TopRight ad
            if (positionId == 1)
            {
                if (ads != null)
                {
                    model = new List<AdViewModel>();
                    foreach (Advertise ad in ads)
                    {
                        model.Add(new AdViewModel()
                        {
                            Id = ad.Id,
                            ImagePath = string.Format("{0}/Advertising/325_310/{1}.jpeg", _filesConfig.BaseUrl, ad.Id),
                            Link = ad.UrlLink,
                            Name = ad.Title
                        });
                    }
                }
                return View("HomeTopRight", model);
            }
            if (positionId == 2)
            {
                return View("HomeTopLeft", model);
            }
            if (positionId == 3)
            {
                return View("HomeBottom", model);
            }
            return View(model);
        }
    }
}
