using DamilolaShopeyin.API.Data;
using DamilolaShopeyin.Core.Models;

namespace DamilolaShopeyin.API.Repositories
{
    public class BlogRepository : RepositoryBase<Blog>
    {
        public BlogRepository(DamilolaShopeyinContext context) : base(context)
        {
        }

    }
}
