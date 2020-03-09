using System;

namespace Kaysho.NET.Core.Contracts.V1.Responses
{
    public class BlogResponse
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }

        public string Snippet { get; set; }

        public string Title { get; set; }


        public string Article { get; set; }

        public string User { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }

    }
}
