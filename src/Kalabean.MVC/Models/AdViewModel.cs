using System;

namespace Kalabean.MVC.Models
{
    public class AdViewModel
    {
        string _basePath;
        public AdViewModel(string basePath) {
            _basePath = basePath;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageOriginalPath { get; set; }
    }
}
