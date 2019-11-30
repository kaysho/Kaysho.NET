using DamilolaShopeyin.Core.Models;
using Kaysho.NET.Core.Models;
using Kaysho.NET.Mobile.Models.Responses;
using System.Threading.Tasks;

namespace Kaysho.NET.Mobile.Contracts.Services.Data
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> Register(Register register);

        Task<AuthenticationResponse> Authenticate(Login login);

        bool IsUserAuthenticated();
    }
}
