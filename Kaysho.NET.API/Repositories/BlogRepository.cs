using DamilolaShopeyin.API.Data;
using DamilolaShopeyin.Core.Models;

namespace DamilolaShopeyin.API.Repositories
{
    public class BlogRepository : RepositoryBase<Blog>
    {
        private readonly DamilolaShopeyinContext _context;
        public BlogRepository(DamilolaShopeyinContext context) : base(context)
        {
            _context = context;
        }

    }
}
