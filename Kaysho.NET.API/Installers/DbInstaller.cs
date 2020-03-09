using DamilolaShopeyin.API.Data;
using DamilolaShopeyin.API.Repositories;
using DamilolaShopeyin.Core.Models;
using DamilolaShopeyin.Core.Services;
using Kaysho.NET.API.Data;
using Kaysho.NET.API.Repositories;
using Kaysho.NET.API.Service;
using Kaysho.NET.Core.Contracts.Repository;
using Kaysho.NET.Core.Contracts.Services.Data;
using Kaysho.NET.Core.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kaysho.NET.API.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<DamilolaShopeyinContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<DamilolaShopeyinContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IRepository<Blog>, BlogRepository>();
            services.AddScoped<IRepository<Comment>, CommentRepository>();
            services.AddScoped<IGenericRepository, GenericRepository>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddSingleton<ICloudStorage, GoogleCloudStorage>();
            services.AddSingleton<WeatherForecastService>();
        }
    }
}
