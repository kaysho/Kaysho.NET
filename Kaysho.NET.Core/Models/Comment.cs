using System;

namespace DamilolaShopeyin.Core.Models
{
    public class Comment : BaseEntity
    {
        public int BlogId { get; set; }
        public string Comments { get; set; }
        public DateTime DateOfComment { get; set; } = DateTime.Now;

        public string UserId { get; set; }

    }
}
