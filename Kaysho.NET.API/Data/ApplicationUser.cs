using Microsoft.AspNetCore.Identity;

namespace DamilolaShopeyin.API.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
