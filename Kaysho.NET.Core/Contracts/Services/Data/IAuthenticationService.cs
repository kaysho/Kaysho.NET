using DamilolaShopeyin.Core.Models;
using Kaysho.NET.Core.Models;
using Kaysho.NET.Core.Models.Responses;
using System.Threading.Tasks;

namespace Kaysho.NET.Core.Contracts.Services.Data
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> Register(Register register);

        Task<Profile> Authenticate(Login login);

        bool IsUserAuthenticated();
    }
}
