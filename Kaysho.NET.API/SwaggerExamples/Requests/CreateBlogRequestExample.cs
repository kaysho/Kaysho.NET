using Kaysho.NET.Core.Contracts.V1.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace Kaysho.NET.API.SwaggerExamples.Requests
{
    public class CreateBlogRequestExample : IExamplesProvider<CreateBlogRequest>
    {
        public CreateBlogRequest GetExamples()
        {
            return new CreateBlogRequest
            {
                Title = "Blog title goes here",
                Article = "Here is your blog article"
            };
        }
    }
}
