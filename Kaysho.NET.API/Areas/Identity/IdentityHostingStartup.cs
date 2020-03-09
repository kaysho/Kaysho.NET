using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(DamilolaShopeyin.API.Areas.Identity.IdentityHostingStartup))]
namespace DamilolaShopeyin.API.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}