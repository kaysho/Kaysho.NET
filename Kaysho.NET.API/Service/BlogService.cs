using Blazored.LocalStorage;
using DamilolaShopeyin.Core.Models;
using Kaysho.NET.Core.Constants;
using Kaysho.NET.Core.Contracts.Repository;
using Kaysho.NET.Core.Contracts.Services.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kaysho.NET.API.Service
{
    public class BlogService : IBlogService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly ILocalStorageService _localStorageService;
        public BlogService(IGenericRepository genericRepository, ILocalStorageService localStorageService)
        {
            _genericRepository = genericRepository;
            _localStorageService = localStorageService;
        }

        public async Task DeleteBlog(int id)
        {
            var token = await _localStorageService.GetItemAsync<string>("token");
            await _genericRepository.DeleteAsync(ApiConstants.BaseApiUrl + ApiConstants.BlogEndpoint + "/" + id, token);
        }

        public async Task<IEnumerable<Blog>> GetAllBlogsAsync()
        {
            var blogs = await _genericRepository.GetAsync<List<Blog>>(ApiConstants.BaseApiUrl + ApiConstants.BlogEndpoint);

            return blogs;
        }


    }
}