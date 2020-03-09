using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Kaysho.NET.API.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kaysho.NET.API.Installers
{
    public class BlazorInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddBlazoredLocalStorage();
            services.AddBlazoredSessionStorage();


            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
        }
    }
}
