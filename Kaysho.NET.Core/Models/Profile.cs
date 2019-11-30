namespace DamilolaShopeyin.Core.Models
{
    public class Profile
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsVerified { get; set; }
        public bool IsError { get; set; }
    }
}
