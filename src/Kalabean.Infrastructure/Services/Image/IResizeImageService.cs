using Kalabean.Domain.Requests.ResizeImage;
using Kalabean.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Infrastructure.Services.Image
{
    public interface IResizeImageService
    {
        Task<ImageResizeResponse> Resize(GetImageRequest request);
    }
}
