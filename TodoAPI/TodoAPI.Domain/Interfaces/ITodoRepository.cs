using TodoAPI.Domain.Models;

namespace TodoAPI.Domain.Interfaces
{
    public interface ITodoRepository : IEntityRepository<Todo>
    {
        Task<IEnumerable<Todo>> GetTodosByUserId(int userId);
    }
}
