using Kalabean.Domain.Responses;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.MVC.Models
{
    public class ListProductViewModel
    {

        public ListProductViewModel(string basePath)
        {
            Current = new ProductViewModel(basePath);
        }
        public List<ProductViewModel> Lst { get; set; } = new List<ProductViewModel>();
        public List<SelectListItem> LstTargtType { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
        public ProductViewModel Current { get; set; }
    }
}
