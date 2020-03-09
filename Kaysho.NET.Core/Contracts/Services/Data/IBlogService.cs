using DamilolaShopeyin.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kaysho.NET.Core.Contracts.Services.Data
{
    public interface IBlogService
    {
        Task<IEnumerable<Blog>> GetAllBlogsAsync();

        Task DeleteBlog(int id);
    }
}
