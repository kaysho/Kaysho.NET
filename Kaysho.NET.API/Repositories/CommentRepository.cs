using DamilolaShopeyin.API.Data;
using DamilolaShopeyin.API.Repositories;
using DamilolaShopeyin.Core.Models;

namespace Kaysho.NET.API.Repositories
{
    public class CommentRepository : RepositoryBase<Comment>
    {
        private readonly DamilolaShopeyinContext _context;
        public CommentRepository(DamilolaShopeyinContext context) : base(context)
        {
            _context = context;
        }

    }
}
