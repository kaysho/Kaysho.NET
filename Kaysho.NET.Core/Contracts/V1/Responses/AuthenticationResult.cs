using DamilolaShopeyin.Core.Models;
using System.Collections.Generic;

namespace Kaysho.NET.Core.Contracts.V1.Responses
{
    public class AuthenticationResult
    {
        public string Token { get; set; }

        public bool Success { get; set; }

        public Profile Profile { get; set; }

        public string Role { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }
}
