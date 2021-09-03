using System;

namespace Kalabean.MVC.Models
{
    public class ShoppingCenterViewModel
    {
        string _basePath;
        public ShoppingCenterViewModel(string basePath)
        {
            _basePath = basePath;
        }
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public int StoresCount { get; set; }
        public string ImagePath
        {
            get
            {
                return string.Format("{0}/Sh_C/475_235/{1}.jpeg", this._basePath, this.Id);
            }
        }

    }
}
