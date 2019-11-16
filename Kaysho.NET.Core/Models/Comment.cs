using System;

namespace DamilolaShopeyin.Core.Models
{
    public class Comment : BaseEntity
    {
        public string Comments { get; set; }
        public DateTime DateOfComment { get; set; }

        public string UserId { get; set; }

    }
}
