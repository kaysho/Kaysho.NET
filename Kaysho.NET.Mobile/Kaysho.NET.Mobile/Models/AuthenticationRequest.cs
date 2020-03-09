namespace Kaysho.NET.Mobile.Models
{
    public class AuthenticationRequest
    {
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
