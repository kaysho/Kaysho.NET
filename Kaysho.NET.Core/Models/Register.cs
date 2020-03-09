using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Kaysho.NET.Core.Models
{
    public class Register
    {
        [Required]
        [JsonProperty("email")]
        public string Email { get; set; }

        [Required]
        [JsonProperty("password")]
        public string Password { get; set; }

        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
