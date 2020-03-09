using Kaysho.NET.Core.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DamilolaShopeyin.Core.Services
{
    public interface IRepository<T>
    {
        T Get(int? id);
        IEnumerable<T> GetAll(PaginationFilter paginationFilter = null);
        T Add(T entity);
        T Delete(int? id);
        T Update(T entity);
        Task<bool> Commit();
    }
}
