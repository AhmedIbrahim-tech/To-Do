using Microsoft.EntityFrameworkCore;
using TodoAPI.Domain.Interfaces;
using TodoAPI.Domain.Models;

namespace TodoAPI.Repository.Data.Repository
{
    public class TodoRepository :EntityRepository<Todo>, ITodoRepository
    {
        private readonly AppDbContext db;
        public TodoRepository(AppDbContext _db):base(_db){
            db = _db;
        }
        public async Task<IEnumerable<Todo>> GetTodosByUserId(int userId)
        {
            //IQueryable<Todo> query = db.Set<Todo>().Where(todo => todo.CustomUserId == userId);
            //return await query.ToListAsync();
            return await db.Todos
               .Where(todo => todo.CustomUserId == userId)
               .OrderBy(todo => todo.Id)
               .ToListAsync();

        }

    }
}
