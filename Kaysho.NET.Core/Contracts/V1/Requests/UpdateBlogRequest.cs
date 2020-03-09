namespace Kaysho.NET.Core.Contracts.V1.Requests
{
    public class UpdateBlogRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Article { get; set; }
    }
}
