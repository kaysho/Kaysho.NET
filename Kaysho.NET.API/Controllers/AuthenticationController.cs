using DamilolaShopeyin.API.Data;
using DamilolaShopeyin.API.Filters;
using DamilolaShopeyin.Core.Models;
using Kaysho.NET.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DamilolaShopeyin.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private ILogger<AuthenticationController> _logger;
        private readonly TokenManagement _tokenManagement;
        private UserManager<ApplicationUser> _userMgr;
        private IPasswordHasher<ApplicationUser> _hasher;
        private readonly IEmailSender _emailSender;
        public AuthenticationController(
            ILogger<AuthenticationController> logger,
            IPasswordHasher<ApplicationUser> hasher,
            IOptions<TokenManagement> tokenManagement,
            UserManager<ApplicationUser> userMgr,
            IEmailSender emailSender
            )
        {
            _logger = logger;
            _userMgr = userMgr;
            _tokenManagement = tokenManagement.Value;
            _hasher = hasher;
            _emailSender = emailSender;


        }

        [AllowAnonymous]
        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userMgr.FindByNameAsync(login.Email);

            if (user != null)
            {
                if (!user.EmailConfirmed)
                {
                    if (_hasher.VerifyHashedPassword(user, user.PasswordHash, login.Password) == PasswordVerificationResult.Success)
                    {
                        var claim = new[]
                        {
                        new Claim(ClaimTypes.Name, login.Email)
                    };
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
                        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var jwtToken = new JwtSecurityToken(
                            _tokenManagement.Issuer,
                            _tokenManagement.Audience,
                            claim,
                            expires: DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration),
                            signingCredentials: credentials
                        );

                        var profile = new Profile
                        {
                            Name = user.Name,
                            Email = user.Email,
                            Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                            Id = user.Id,
                            IsDeleted = false,
                            IsVerified = true,
                            IsError = false
                        };


                        return Ok(profile);

                    }
                }
                else
                {
                    return BadRequest("Confirm Email first");
                }


            }

            return BadRequest("Invalid Request");
        }


        [AllowAnonymous]
        [HttpPost, Route("register")]
        public async Task<IActionResult> Register([FromBody] Register register)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser { UserName = register.Email, Email = register.Email, Name = register.Name };
            var result = await _userMgr.CreateAsync(user, register.Password);

            if (result.Succeeded)
            {
                //await _emailSender.SendEmailAsync(register.Email, "Hello", "Helloooo");
                _logger.LogInformation("User created succesddfully");

                return Ok("User created succesfully");
            }

            return BadRequest(result);
        }
    }
}