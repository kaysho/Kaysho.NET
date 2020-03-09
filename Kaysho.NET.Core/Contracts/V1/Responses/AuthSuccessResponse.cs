using DamilolaShopeyin.Core.Models;

namespace Kaysho.NET.Core.Contracts.V1.Responses
{
    public class AuthSuccessResponse
    {

        public Profile Data { get; set; }

        public string Message { get; set; }

        public bool Error { get; set; }

        public string Token { get; set; }
        public string Role { get; set; }

        public AuthSuccessResponse() { }

        public AuthSuccessResponse(Profile response, string message, bool error, string token, string role)
        {
            Data = response;
            Message = message;
            Error = error;
            Token = token;
            Role = role;
        }
    }
}
