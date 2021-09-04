using System;

namespace Kalabean.MVC.Models
{
    public class ShoppingCenterServiceModel
    {
        string _basePath;
        public ShoppingCenterServiceModel(string basePath)
        {
            _basePath = basePath;
        }
        public string Name { get; set; }
        public int Id { get; set; }
        public string ImageIcon
        {
            get
            {
                return string.Format("{0}/Sh_C/475_235/{1}.jpeg", this._basePath, this.Id);
            }
        }

        public string ImagePath
        {
            get
            {
                return string.Format("{0}/Sh_C/475_235/{1}.jpeg", this._basePath, this.Id);
            }
        }

    }
}
