using AutoMapper;
using DamilolaShopeyin.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Kaysho.NET.API.Controllers.V1
{
    public abstract class BaseController : ControllerBase
    {
        public readonly IHttpContextAccessor httpContextAccessor;
        public readonly UserManager<ApplicationUser> _userMgr;
        public readonly IMapper mapper;

        public BaseController(UserManager<ApplicationUser> userMgr, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _userMgr = userMgr;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
        }
    }
}
