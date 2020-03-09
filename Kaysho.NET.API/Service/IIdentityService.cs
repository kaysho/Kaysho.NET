using DamilolaShopeyin.Core.Models;
using Kaysho.NET.Core.Contracts.V1.Responses;
using Kaysho.NET.Core.Models;
using System.Threading.Tasks;

namespace Kaysho.NET.API.Service
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(Register register);

        Task<AuthenticationResult> LoginAsync(Login login);
    }
}
