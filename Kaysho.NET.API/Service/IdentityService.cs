using AutoMapper;
using DamilolaShopeyin.API.Data;
using DamilolaShopeyin.API.Filters;
using DamilolaShopeyin.Core.Models;
using Kaysho.NET.Core.Constants;
using Kaysho.NET.Core.Contracts.V1.Responses;
using Kaysho.NET.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Profile = DamilolaShopeyin.Core.Models.Profile;

namespace Kaysho.NET.API.Service
{
    public class IdentityService : IIdentityService
    {
        protected readonly LinkGenerator _linkGenerator;
        private readonly IMapper mapper;
        private readonly TokenManagement _tokenManagement;
        private UserManager<ApplicationUser> _userMgr;
        private RoleManager<IdentityRole> _roleMgr;
        private IPasswordHasher<ApplicationUser> _hasher;
        private readonly IEmailSender _emailSender;
        private readonly IUriService _uriService;


        public IdentityService(
            IPasswordHasher<ApplicationUser> hasher,
            IOptions<TokenManagement> tokenManagement,
            UserManager<ApplicationUser> userMgr,
            RoleManager<IdentityRole> roleMgr,
            IEmailSender emailSender,
            LinkGenerator linkGenerator,
            IMapper mapper,
            IUriService uriService
            )
        {
            _uriService = uriService;
            _userMgr = userMgr;
            _roleMgr = roleMgr;
            _tokenManagement = tokenManagement.Value;
            _hasher = hasher;
            _emailSender = emailSender;
            _linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        public async Task<AuthenticationResult> LoginAsync(Login login)
        {
            var user = await _userMgr.FindByNameAsync(login.Email);

            if (user == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User does not exist" }
                };
            }

            var userHasValidPassword = await _userMgr.CheckPasswordAsync(user, login.Password);

            if (!userHasValidPassword)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User/password combination is wrong" }
                };
            }

            return await GenerateAuthenticationResultForUserAsync(user);
        }

        public async Task<AuthenticationResult> RegisterAsync(Register register)
        {
            var existingUser = await _userMgr.FindByEmailAsync(register.Email);

            if (existingUser != null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User with this email address already exists" }
                };
            }

            var user = new ApplicationUser { UserName = register.Email, Email = register.Email, Name = register.Name };
            var result = await _userMgr.CreateAsync(user, register.Password);


            if (result.Succeeded)
            {
                await _userMgr.AddToRoleAsync(user, RoleConstants.UserRole);
                var code = await _userMgr.GenerateEmailConfirmationTokenAsync(user);


                var b = _uriService.GetAuthenticationUri(user.Id);


                await _emailSender.SendEmailAsync(register.Email, "Confirm your account", $"Please confirm your account by clicking this link: <a href='{b}'>link</a>");

                return new AuthenticationResult
                {
                    Success = true
                };
            }
            if (!result.Succeeded)
            {
                return new AuthenticationResult
                {
                    Success = false,
                    Errors = result.Errors.Select(x => x.Description)
                };
            }

            return new AuthenticationResult
            {
                Success = true
            };
        }

        private void AddRolesToClaims(List<Claim> claims, IEnumerable<string> roles)
        {
            foreach (var role in roles)
            {
                var roleClaim = new Claim(ClaimTypes.Role, role);
                claims.Add(roleClaim);
            }
        }

        private async Task<AuthenticationResult> GenerateAuthenticationResultForUserAsync(ApplicationUser user)
        {

            var claim = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };

            // Get User roles and add them to claims
            var roles = await _userMgr.GetRolesAsync(user);
            AddRolesToClaims(claim, roles);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                _tokenManagement.Issuer,
                _tokenManagement.Audience,
                claim,
                expires: DateTime.Now.AddDays(_tokenManagement.AccessExpiration),
                signingCredentials: credentials
            );

            return new AuthenticationResult
            {
                Success = true,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                Profile = mapper.Map<Profile>(user),
                Role = roles[0]
            };


        }
    }
}
