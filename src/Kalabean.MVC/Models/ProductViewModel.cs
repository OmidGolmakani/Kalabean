using System;

namespace Kalabean.MVC.Models
{
    public class ProductViewModel
    {
        string _basePath;
        public ProductViewModel(string basePath) {
            _basePath = basePath;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageListPath
        {
            get
            {
                return string.Format("{0}/Products/243_201/{1}.jpeg", this._basePath, this.Id);
            }
        }
    }
}
