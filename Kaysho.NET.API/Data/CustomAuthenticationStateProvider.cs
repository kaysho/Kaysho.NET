using Blazored.LocalStorage;
using DamilolaShopeyin.Core.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Kaysho.NET.API.Data
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private ILocalStorageService _localStorageService { get; }

        public CustomAuthenticationStateProvider(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {

            var accessToken = await _localStorageService.GetItemAsync<string>("emailAddress");
            var role = await _localStorageService.GetItemAsync<string>("role");

            ClaimsIdentity identity;

            if (accessToken != null && accessToken != string.Empty)
            {

                identity = new ClaimsIdentity(new[]
                {
                        new Claim(ClaimTypes.Name, accessToken),
                        new Claim(ClaimTypes.Role, role)

                }, "apiauth_type");
            }
            else
            {
                identity = new ClaimsIdentity();
            }

            var claimsPrincipal = new ClaimsPrincipal(identity);

            return await Task.FromResult(new AuthenticationState(claimsPrincipal));

        }

        public void MarkUserAsAuthenticated(Profile profile)
        {

            _localStorageService.SetItemAsync("emailAddress", profile.Email);
            //_localStorageService.SetItemAsync("token", profile.Token);
            //_localStorageService.SetItemAsync("role", profile.Role);

            var identity = GetClaimsIdentity(profile);


            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public void MarkUserAsLoggedOut()
        {
            _localStorageService.RemoveItemAsync("emailAddress");
            _localStorageService.RemoveItemAsync("token");
            _localStorageService.RemoveItemAsync("role");

            var identity = new ClaimsIdentity();

            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        private ClaimsIdentity GetClaimsIdentity(Profile profile)
        {
            var claimsIdentity = new ClaimsIdentity(new[]

            {
                new Claim(ClaimTypes.Name, profile.Name)
                //new Claim(ClaimTypes.Role, profile.Role)
            });

            return claimsIdentity;
        }
    }
}
