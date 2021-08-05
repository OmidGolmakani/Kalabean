using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.Product;
using Kalabean.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Domain.Mappers
{
    public interface IProductMapper
    {
        Product Map(AddProductRequest request);
        Product Map(EditProductRequest request);
        ProductResponse Map(Product request);
    }
}
