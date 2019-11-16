using DamilolaShopeyin.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DamilolaShopeyin.API.Data
{
    public class DamilolaShopeyinContext : IdentityDbContext<ApplicationUser>
    {
        public DamilolaShopeyinContext(DbContextOptions<DamilolaShopeyinContext> options) : base(options)
        {

        }

        public DbSet<Blog> Blogs { get; set; }
        //public DbSet<Comment> Comments { get; set; }
    }
}
