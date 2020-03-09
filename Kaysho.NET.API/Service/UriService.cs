using Kaysho.NET.Core.V1.Requests.Queries;
using Microsoft.AspNetCore.WebUtilities;
using System;

namespace Kaysho.NET.API.Service
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetBlogUri(string postId)
        {
            return new Uri(_baseUri + "blogs/" + postId);// + ApiRoutes.Posts.Get.Replace("{postId}", postId));
        }

        public Uri GetAuthenticationUri(string userId)
        {
            return new Uri(_baseUri + "authentication/" + userId);// + ApiRoutes.Posts.Get.Replace("{postId}", postId));
        }

        public Uri GetAllBlogsUri(PaginationQuery pagination = null)
        {
            var uri = new Uri(_baseUri);

            if (pagination == null)
            {
                return uri;
            }

            var modifiedUri = QueryHelpers.AddQueryString(_baseUri, "pageNumber", pagination.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", pagination.PageSize.ToString());

            return new Uri(modifiedUri);
        }
    }
}
