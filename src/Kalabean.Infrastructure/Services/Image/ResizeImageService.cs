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
            if (request.Image == null || request.ImageSize == null) return null;
            var Img = System.Drawing.Image.FromStream(request.Image.OpenReadStream());
            if (Img.Width > Img.Height)
            {
                var w = Img.Width / request.ImageSize.Width;
                request.ImageSize = new System.Drawing.Size()
                {
                    Width = w,
                    Height = Img.Height / w
                };
            }
            else if (Img.Height > Img.Width)
            {
                var h = Img.Height / request.ImageSize.Height;
                request.ImageSize = new System.Drawing.Size()
                {
                    Width = Img.Height / h,
                    Height = h
                };
            }
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
