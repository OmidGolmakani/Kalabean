using Kalabean.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kalabean.MVC.Models
{
    public class ShoppingCenterDeatailViewModel
    {
        string _basePath;
        public ShoppingCenterDeatailViewModel(string basePath)
        {
            _basePath = basePath;
        }
        public ShoppingCenter ShoppingCenterDeatails { get; set; }

        [Display(Name = "عرض جغرافیایی")]
        public string Latitude { get; set; }

        [Display(Name = "طول جغرافیایی")]
        public string Longitude { get; set; }

        public List<StoreViewModel> StoreModels { get; set; }

        public List<string> Services { get; set; }

        public string ImagePath
        {
            get
            {
                return string.Format("{0}/Sh_C/475_235/{1}.jpeg", this._basePath, this.ShoppingCenterDeatails.Id);
            }
        }

    }
}
