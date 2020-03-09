using System;

namespace Kaysho.NET.Core.Dto
{
    public class BlogDto
    {
        public string Image { get; set; }

        public string Snippet { get; set; }

        public string Title { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public string Article { get; set; }

        public string User { get; set; }

    }
}
