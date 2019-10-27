using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DamilolaShopeyin.Core.Models
{
    public class Login
    {
        [Required]
        [JsonProperty("email")]
        public string Email { get; set; }

        [Required]
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
