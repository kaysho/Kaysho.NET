using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kaysho.NET.API.Installers
{
    public class SignalRInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            //add signalr
            services.AddSignalR();
        }
    }
}
