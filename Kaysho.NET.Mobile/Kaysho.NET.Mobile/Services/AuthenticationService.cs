using DamilolaShopeyin.Core.Models;
using Kaysho.NET.Core.Constants;
using Kaysho.NET.Core.Contracts.Repository;
using Kaysho.NET.Core.Contracts.Services.Data;
using Kaysho.NET.Core.Models;
using Kaysho.NET.Core.Models.Responses;
using Kaysho.NET.Mobile.Contracts.Services.General;
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
                Path = ApiConstants.RegisterEndpoint,
                Port = -1
            };

            return await _genericRepository.PostAsync<Register, AuthenticationResponse>(builder.ToString(), register);
        }

        public bool IsUserAuthenticated()
        {
            return !string.IsNullOrEmpty(_settingsService.UserIdSetting);
        }

        public async Task<Profile> Authenticate(Login login)
        {

            return await _genericRepository.PostAsync<Login, Profile>(ApiConstants.BaseApiUrl + ApiConstants.AuthenticateEndpoint, login);
        }

        //Task<bool> IAuthenticationService.IsUserAuthenticated()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
