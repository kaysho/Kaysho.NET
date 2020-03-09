using Kaysho.NET.Core.V1.Requests.Queries;
using System;

namespace Kaysho.NET.API.Service
{
    public interface IUriService
    {
        Uri GetBlogUri(string postId);
        Uri GetAuthenticationUri(string userId);

        Uri GetAllBlogsUri(PaginationQuery pagination = null);
    }
}
