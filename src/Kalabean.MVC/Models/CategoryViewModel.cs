using System;

namespace Kalabean.MVC.Models
{
    public class CategoryViewModel
    {
        string _basePath;
        public CategoryViewModel(string basePath)
        {
            _basePath = basePath;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageOriginalPath { get; set; }
        public string ImageListPat
        {
            get
            {
                return string.Format("{0}/Categories/250_250/{1}.jpeg", this._basePath, this.Id);
            }
        }
        public int StoresCount { get; set; }
        public int ProductsCount { get; set; }
    }
}
