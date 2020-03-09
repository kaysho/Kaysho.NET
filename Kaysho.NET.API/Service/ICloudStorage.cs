using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Kaysho.NET.API.Service
{
    public interface ICloudStorage
    {
        Task<string> UploadFileAsync(IFormFile imageFile, string fileNameForStorage);
        Task DeleteFileAsync(string fileNameForStorage);
    }
}
