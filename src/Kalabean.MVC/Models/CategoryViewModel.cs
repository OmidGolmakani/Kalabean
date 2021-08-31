using System;

namespace Kalabean.MVC.Models
{
    public class CategoryViewModel
    {
        string _basePath;
        public CategoryViewModel(string basePath) {
            _basePath = basePath;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageOriginalPath { get; set; }
    }
}
