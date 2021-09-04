using Kalabean.Domain.Entities;
using Kalabean.Domain.Requests.Category;
using Kalabean.Domain.Responses;
using System.Collections.Generic;

namespace Kalabean.Domain.Mappers
{
    public class CategoryMapper : ICategoryMapper
    {
        public CategoryMapper()
        {
        }

        public Category Map(AddCategoryRequest request)
        {
            if (request == null) return null;

            var category = new Category
            {
                Name = request.Name,
                Order = request.Order,
                Description = request.Description,
                HtmlContent = request.HtmlContent,
                ParentId = request.ParentId,
                HasImage = request.Image != null && request.Image.Length > 0
            };
            return category;
        }

        public Category Map(EditCategoryRequest request)
        {
            if (request == null) return null;

            var category = new Category
            {
                Id = request.Id,
                Name = request.Name,
                Order = request.Order,
                Description = request.Description,
                HtmlContent = request.HtmlContent,
                ParentId = request.ParentId
            };
            if (request.ImageEdited)
            {
                category.HasImage = request.Image != null && request.Image.Length > 0;
            }
            return category;
        }
        public CategoryResponse Map(Category category)
        {
            if (category == null) return null;

            var response = new CategoryResponse
            {
                Id = category.Id,
                Description = category.Description,
                HtmlContent = category.HtmlContent,
                Name = category.Name,
                Order = category.Order,
                ImageUrl = (category.HasImage ?? false) ? $"/KL_ImagesRepo/Categories/250_250/{category.Id}.jpeg" : ""
            };

            if (category.Parent != null)
            {
                response.Parent = getCategoryResponse(category.Parent, true);
            }

            if (category.Children != null &&
                category.Children.Count > 0)
            {
                response.Children = new List<CategoryResponse>();
                foreach (Category child in category.Children)
                    response.Children.Add(getCategoryResponse(child, true));
            }
            return response;
        }

        public ThumbResponse<int> MapThumb(Category request)
        {
            if (request == null) return null;
            var response = new ThumbResponse<int>()
            {
                Id = request.Id,
                Name = request.Name
            };
            return response;
        }

        private CategoryResponse getCategoryResponse(Category category, bool notDeleted)
        {
            if (!notDeleted || category.IsDeleted)
                return null;
            return new CategoryResponse
            {
                Id = category.Id,
                Description = category.Description,
                HtmlContent = category.HtmlContent,
                Name = category.Name,
                Order = category.Order,
                ImageUrl = (category.HasImage ?? false) ? $"/KL_ImagesRepo/Categories/250_250/{category.Id}.jpeg" : ""

            };
        }
    }
}
