using System;

namespace Kalabean.MVC.Models
{
    public class StoreViewModel
    {
        string _basePath;
        public StoreViewModel(string basePath)
        {
            _basePath = basePath;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductsCount { get; set; }
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
        public CategoryViewModel Category { get; set; }
    }
}
