using Kalabean.Domain.Requests.ResizeImage;
using Kalabean.Domain.Responses;
using Microsoft.AspNetCore.Hosting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalabean.Infrastructure.Services.Image
{
    public class ResizeImageService<T> : IResizeImageService<T>
    {
        private readonly IWebHostEnvironment env;

        public ResizeImageService(IWebHostEnvironment env)
        {
            this.env = env;
        }

        public async Task<ImageResizeResponse> Resize(GetImageRequest<T> request)
        {

            if (request == null || request.ImageUrl == null) return null;
            if (request.ImageUrl == null || request.ImageSize == null) return null;
            request.ImageUrl = System.IO.Path.Combine(env.ContentRootPath, request.ImageUrl);
            var Img = System.Drawing.Image.FromFile(request.ImageUrl);
            if (Img.Width > Img.Height)
            {
                request.ImageSize = new System.Drawing.Size(request.ImageSize.Width, 0);
            }
            if (Img.Width < Img.Height)
            {
                request.ImageSize = new System.Drawing.Size(0, request.ImageSize.Height);
            }
            var OldFile = request.ImageUrl;
            var FileDir = System.IO.Path.GetDirectoryName(request.ImageUrl);
            var NewFile = System.IO.Path.GetFileName(request.ImageUrl);
            using var image = SixLabors.ImageSharp.Image.Load(request.ImageUrl);
            {
                image.Mutate(x => x.Resize(request.ImageSize.Width, request.ImageSize.Height));
                if (Directory.Exists(Path.Combine(FileDir, request.Folder)) == false)
                {
                    Directory.CreateDirectory(Path.Combine(FileDir, request.Folder));
                }
                image.Save(System.IO.Path.Combine(FileDir, request.Folder, NewFile));
            }
            if (Img != null)
                Img.Dispose();
            return new ImageResizeResponse()
            {
                FullPath = System.IO.Path.GetFullPath(request.ImageUrl),
                FileName = request.ImageUrl,
                Size = request.ImageSize
            };
        }
    }
}
