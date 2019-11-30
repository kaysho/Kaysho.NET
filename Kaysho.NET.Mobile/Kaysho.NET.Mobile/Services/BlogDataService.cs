using Akavache;
using Kaysho.NET.Mobile.Constants;
using Kaysho.NET.Mobile.Contracts.Repository;
using Kaysho.NET.Mobile.Contracts.Services.Data;
using Kaysho.NET.Mobile.Models.Navigation;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Kaysho.NET.Mobile.Services
{
    public class BlogDataService : BaseService, IBlogDataService
    {
        private readonly IGenericRepository _genericRepository;

        public BlogDataService(IGenericRepository genericRepository, IBlobCache cache = null) : base(cache)
        {
            _genericRepository = genericRepository;
        }

        public async Task<IEnumerable<NavigationModel>> GetAllBlogsAsync()
        {
            List<NavigationModel> piesFromCache = await GetFromCache<List<NavigationModel>>(CacheNameConstants.AllPies);

            if (piesFromCache != null)//loaded from cache
            {
                return piesFromCache;
            }
            else
            {
                UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
                {
                    Path = ApiConstants.RegisterEndpoint
                };

                var pies = await _genericRepository.GetAsync<List<NavigationModel>>(builder.ToString());

                await Cache.InsertObject(CacheNameConstants.AllPies, pies, DateTimeOffset.Now.AddSeconds(20));

                return pies;
            }
        }
    }
}
