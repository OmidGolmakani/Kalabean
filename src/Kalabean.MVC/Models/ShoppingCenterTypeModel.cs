using System;
using System.Collections.Generic;

namespace Kalabean.MVC.Models
{
    public class ShoppingCenterTypeModel
    {
        string _basePath;
        public ShoppingCenterTypeModel(string basePath)
        {
            _basePath = basePath;
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImagePath
        {
            get
            {
                return string.Format("{0}/Sh_C_Types/100_100/{1}.jpeg", this._basePath, this.Id);
            }
        }
    }
}
