using Blazored.LocalStorage;
using Blazored.SessionStorage;
using DamilolaShopeyin.Core.Models;
using Kaysho.NET.Core.Constants;
using Kaysho.NET.Core.Contracts.Repository;
using Kaysho.NET.Core.Contracts.Services.Data;
using Kaysho.NET.Core.Models;
using Kaysho.NET.Core.Models.Responses;
using System;
using System.Threading.Tasks;

namespace Kaysho.NET.API.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly ISessionStorageService _sessionStorage;
        private readonly ILocalStorageService _localStorage;
        public AuthenticationService(IGenericRepository genericRepository, ISessionStorageService sessionStorage, ILocalStorageService localStorageService)
        {
            _sessionStorage = sessionStorage;
            _genericRepository = genericRepository;
            _localStorage = localStorageService;

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

        //public async Task<bool> IsUserAuthenticated()
        //{
        //    return !string.IsNullOrEmpty(await _sessionStorage.GetItemAsync<string>("token"));
        //}

        public async Task<Profile> Authenticate(Login login)
        {
            return await _genericRepository.PostAsync<Login, Profile>(ApiConstants.BaseApiUrl + ApiConstants.AuthenticateEndpoint, login);
        }

        public bool a()
        {
            var c = _sessionStorage.GetItemAsync<string>("");
            return false;
        }

        public bool IsUserAuthenticated()
        {
            return true;
            //return !string.IsNullOrEmpty(_localStorage.GetItemAsync<string>("token"));
        }
    }
}
