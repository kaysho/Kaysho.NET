using DamilolaShopeyin.Core.Models;
using System.Collections.Generic;

namespace DamilolaShopeyin.API
{
    public interface IBlogRepository
    {
        Blog GetBlog(int? id);
        IEnumerable<Blog> GetAllBlogs();
        Blog Add(Blog blog);
        Blog Delete(int? id);
        Blog Update(Blog blog);
    }
}
