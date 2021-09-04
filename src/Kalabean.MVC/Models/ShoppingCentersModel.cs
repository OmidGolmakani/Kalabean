using System;
using System.Collections.Generic;

namespace Kalabean.MVC.Models
{
    public class ShoppingCentersModel
    {
        string _basePath;
        public ShoppingCentersModel(string basePath)
        {
            _basePath = basePath;
        }
        public List<ShoppingCenterViewModel> ShoppingCenters { get; set; }
        public List<ShoppingCenterTypeModel> Types { get; set; }
    }
}
