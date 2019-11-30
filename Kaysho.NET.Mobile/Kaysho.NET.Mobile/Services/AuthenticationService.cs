using DamilolaShopeyin.Core.Models;
using Kaysho.NET.Core.Models;
using Kaysho.NET.Mobile.Constants;
using Kaysho.NET.Mobile.Contracts.Repository;
using Kaysho.NET.Mobile.Contracts.Services.Data;
using Kaysho.NET.Mobile.Contracts.Services.General;
using Kaysho.NET.Mobile.Models.Responses;
using System;
using System.Threading.Tasks;

namespace Kaysho.NET.Mobile.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly ISettingsService _settingsService;
        public AuthenticationService(IGenericRepository genericRepository, ISettingsService settingsService)
        {
            _settingsService = settingsService;
            _genericRepository = genericRepository;

        }

        public async Task<AuthenticationResponse> Register(Register register)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.RegisterEndpoint
            };

            return await _genericRepository.PostAsync<Register, AuthenticationResponse>(builder.ToString(), register);
        }

        public bool IsUserAuthenticated()
        {
            return !string.IsNullOrEmpty(_settingsService.UserIdSetting);
        }

        public async Task<AuthenticationResponse> Authenticate(Login login)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AuthenticateEndpoint
            };
            return await _genericRepository.PostAsync<Login, AuthenticationResponse>(builder.ToString(), login);
        }
    }
}
