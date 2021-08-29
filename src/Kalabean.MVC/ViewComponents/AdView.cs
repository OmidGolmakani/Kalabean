using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kalabean.MVC.ViewComponents
{
    public class AdView: ViewComponent
    {
        public AdView() { }
        public async Task<IViewComponentResult> InvokeAsync(bool isDone)
        {
            return View(isDone);
        }
    }
}
