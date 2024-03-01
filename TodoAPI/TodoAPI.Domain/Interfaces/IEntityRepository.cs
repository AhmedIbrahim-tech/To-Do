using System.Linq.Expressions;
using TodoAPI.Domain.Models;

namespace TodoAPI.Domain.Interfaces
{
    public interface IEntityRepository<T> where T : class, IEntityBase, new()
    {
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includeProperties);
        Task<int> AddAsync(T entity);
        Task<int> DeleteAsync(int id);
        Task<int> UpdateAsync(int id, T entity);
        Task<IEnumerable<T>> GetAllAsync(int id);
    }
}
