using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Infrastructure.AppSettingConfigs.Images
{
    public class ImageSize
    {
        public ImageType ImageType { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<ImageSize> ImageSizes { get; set; }
    }
    public enum ImageType { City = 1, Product = 2 }
}
