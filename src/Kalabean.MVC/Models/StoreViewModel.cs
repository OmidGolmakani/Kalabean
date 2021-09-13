using System;
using System.Collections.Generic;

namespace Kalabean.MVC.Models
{
    public class StoreViewModel
    {
        string _basePath;
        public StoreViewModel(string basePath)
        {
            _basePath = basePath;
            Category = new CategoryViewModel(_basePath);
            Products = new List<ProductViewModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Telegram { get; set; }
        public string Instagram { get; set; }
        public int ProductsCount { get; set; }
        public string ImageProfile
        {
            get
            {
                return string.Format("{0}/Stores/250_250/{1}.jpeg", this._basePath, this.Id);
            }
        }
        public string ImageListPath
        {
            get
            {
                return string.Format("{0}/Stores/255_180/{1}.jpeg", this._basePath, this.Id);
            }
        }
        public string ImageFeatured
        {
            get
            {
                return string.Format("{0}/Stores/435_185/{1}.jpeg", this._basePath, this.Id);
            }
        }
        public string CityName { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }
        public string WorkingHours { get; set; }
        public string VirtualTour { get; set; }
        public float? DiscountPercentage { get; set; }
        public CategoryViewModel Category { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
