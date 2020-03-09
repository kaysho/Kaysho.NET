using System.Collections.Generic;

namespace Kaysho.NET.Core.Contracts.V1.Responses
{
    public class AuthFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
