using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.Article;
using Kalabean.Domain.Responses;
using System;

namespace Kalabean.Domain.Mappers
{
    public class ArticleMapper : IArticleMapper
    {
        private readonly IUserMapper user;

        public ArticleMapper(IUserMapper User)
        {
            user = User;
        }

        public Article Map(AddArticleRequest request)
        {
            if (request == null) return null;

            var Article = new Article
            {
                Description = request.Description,
                Name = request.Name,
                CreatedDate = DateTime.Now,
                IsDeleted = false,
                KeyWords = request.KeyWords,
                ShowInPortal = request.ShowInPortal,
                SuggestedContent = request.SuggestedContent,
                Summary = request.Summary
            };

            Article.HasImage = request.Image != null && request.Image.Length > 0;
            Article.HasFile = request.File != null && request.File.Length > 0;
            Article.FileExtention = request.File != null && request.File.Length > 0 ? System.IO.Path.GetExtension(request.File.FileName) : "";
            return Article;
        }

        public Article Map(EditArticleRequest request)
        {
            if (request == null) return null;

            var Article = new Article
            {
                Description = request.Description,
                Name = request.Name,
                Id = request.Id,
                LastModified = DateTime.Now,
                IsDeleted = false,
                KeyWords = request.KeyWords,
                ShowInPortal = request.ShowInPortal,
                SuggestedContent = request.SuggestedContent,
                Summary = request.Summary
            };

            if (request.ImageEdited)
            {
                Article.HasImage = request.Image != null && request.Image.Length > 0;
            }
            if (request.FileEdited)
            {
                Article.HasFile = request.File != null && request.File.Length > 0;
                Article.FileExtention = request.File != null && request.File.Length > 0 ? System.IO.Path.GetExtension(request.File.FileName) : "";
            }

            return Article;
        }
        public ArticleResponse Map(Article Article)
        {
            if (Article == null) return null;

            var response = new ArticleResponse
            {
                Id = Article.Id,
                Description = Article.Description,
                Name = Article.Name,
                CreatedDate = Article.CreatedDate,
                LastModified = Article.LastModified,
                KeyWords = Article.KeyWords,
                ShowInPortal = Article.ShowInPortal,
                SuggestedContent = Article.SuggestedContent,
                Summary = Article.Summary,
                AdminThumb = user.MapThumb(Article.AdminUser)

            };
            if (Article.HasImage)
                response.ImageUrl = $"/KL_ImagesRepo/Articles/250_250/{Article.Id}.jpeg";
            if (Article.HasFile)
            {
                response.FileUrl = $"/KL_ImagesRepo/Files/Articles/{Article.Id}{Article.FileExtention}";
            }
            return response;
        }

        public ThumbResponse<long> MapThumb(Article request)
        {
            if (request == null) return null;
            return new ThumbResponse<long>()
            {
                Id = request.Id,
                Name = request.Name
            };
        }
    }
}
