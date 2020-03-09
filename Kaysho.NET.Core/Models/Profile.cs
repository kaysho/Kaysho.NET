using Newtonsoft.Json;

namespace DamilolaShopeyin.Core.Models
{
    public class Profile
    {

        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("isVerified")]
        public bool IsVerified { get; set; }

    }
}
