using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DamilolaShopeyin.Core.Models
{
    public class Blog : BaseEntity
    {
        public string Image { get; set; }
        public string Snippet { get; set; }
        [Required]
        public string Title { get; set; }
        public string Date { get; set; }

        public string Article { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
