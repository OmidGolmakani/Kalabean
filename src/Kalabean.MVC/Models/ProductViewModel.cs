using System;

namespace Kalabean.MVC.Models
{
    public class ProductViewModel
    {
        string _basePath;
        public ProductViewModel(string basePath) {
            _basePath = basePath;
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string FormattedPrice { get; set; }
        public string ImageListPath
        {
            get
            {
                return string.Format("{0}/Products/245_205/{1}.jpeg", this._basePath, this.Id);
            }
        }
        
    }
}
