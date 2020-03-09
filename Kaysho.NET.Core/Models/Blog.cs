using Kaysho.NET.Core.CustomValidationAttribute;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DamilolaShopeyin.Core.Models
{
    public class Blog : BaseEntity
    {
        public string ImageUrl { get; set; }

        public string ImageStorageName { get; set; }


        public string Snippet { get; set; }

        [Required]
        public string Title { get; set; }



        [Required]
        public string Article { get; set; }

        [JsonIgnore]
        public string User { get; set; }

        [MaxFileSize(1 * 1024 * 1024)]
        [PermittedExtensions(new string[] { ".jpg", ".png", ".gif", ".jpeg" })]
        [NotMapped]
        [JsonIgnore]
        public virtual IFormFile ImageFile { get; set; }


        [JsonIgnore]
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
