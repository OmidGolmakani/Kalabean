using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Requests.PossibilitiesShopCenter
{
    public class EditPossibilitiesShopCenterRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool ImageEdited { get; set; }
        public IFormFile Image { get; set; }
    }
}
