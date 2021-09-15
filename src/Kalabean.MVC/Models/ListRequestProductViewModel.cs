using Kalabean.Domain.Requests.Requirement;
using Kalabean.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.MVC.Models
{
    public class ListRequestProductViewModel
    {
        public IEnumerable<CategoryResponse> Categories { get; set; } = new List<CategoryResponse>();
        public IEnumerable<CityResponse> Cities { get; set; } = new List<CityResponse>();
        public RequestProductViewModel RequestProduct { get; set; }
    }
}
