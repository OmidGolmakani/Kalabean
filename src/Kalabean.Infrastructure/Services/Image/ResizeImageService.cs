using Kalabean.Domain.Requests.ResizeImage;
using Kalabean.Domain.Responses;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Infrastructure.Services.Image
{
    public class ResizeImageService : IResizeImageService
    {
        public Task<ImageResizeResponse> Resize(GetImageRequest request)
        {
            if (request == null || request.Image == null) return null;
            using var image = SixLabors.ImageSharp.Image.Load(request.Image.OpenReadStream());
            image.Mutate(x => x.Resize(request.ImageSize.Width, request.ImageSize.Height));
            image.Save(request.Image.FileName);
            return Task.FromResult(new ImageResizeResponse()
            {
                FullPath = System.IO.Path.GetFullPath(request.Image.FileName),
                FileName = request.Image.FileName,
                Size = request.ImageSize
            });
        }
    }
}
