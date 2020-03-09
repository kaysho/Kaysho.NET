using Kaysho.NET.Core.Contracts.V1.Responses;
using Swashbuckle.AspNetCore.Filters;
using System;

namespace Kaysho.NET.API.SwaggerExamples.Responses
{
    public class BlogResponseExample : IExamplesProvider<BlogResponse>
    {
        public BlogResponse GetExamples()
        {
            return new BlogResponse
            {
                Id = 1,
                Article = "Blog article....",
                Title = "Blog title...",
                UpdatedAt = DateTime.Now,
                CreatedAt = DateTime.Now,
                ImageUrl = "http://image.png",
                IsDeleted = false,
                Snippet = "Blog snippet...",
                User = "User id"
            };
        }
    }
}
