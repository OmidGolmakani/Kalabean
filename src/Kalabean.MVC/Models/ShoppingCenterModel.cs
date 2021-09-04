using System;
using System.Collections.Generic;

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
        public string ImageProfile
        {
            get
            {
                return string.Format("{0}/Sh_C/250_250/{1}.jpeg", this._basePath, this.Id);
            }
        }
        public string Tel { get; set; }
        public string WorkingHours { get; set; }
        public string VirtualTourUrl { get; set; }
        public string Website { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }
        public List<StoreViewModel> Stores { get; set; }
        public List<ShoppingCenterServiceModel> Services { get; set; }
        public List<FloorViewModel> Floors { get; set; }
    }
}
