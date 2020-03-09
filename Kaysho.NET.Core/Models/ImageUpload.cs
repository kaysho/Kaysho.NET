using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kaysho.NET.Core.Models
{
    public class ImageUpload
    {
        [FromForm(Name = "avatar")]
        public IFormFile Avatar { get; set; }
    }
}
