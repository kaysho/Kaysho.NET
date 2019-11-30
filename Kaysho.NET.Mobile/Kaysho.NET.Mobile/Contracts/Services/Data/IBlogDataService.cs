using Kaysho.NET.Mobile.Models.Navigation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kaysho.NET.Mobile.Contracts.Services.Data
{
    public interface IBlogDataService
    {
        Task<IEnumerable<NavigationModel>> GetAllBlogsAsync();
    }
}
