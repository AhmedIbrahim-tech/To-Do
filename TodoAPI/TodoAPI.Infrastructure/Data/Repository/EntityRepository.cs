
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using TodoAPI.Domain.Interfaces;

namespace TodoAPI.Repository.Data.Repository
{
    public class EntityRepository<T> : IEntityRepository<T> where T : class, IEntityBase, new()
    {
        private readonly AppDbContext db;

        public EntityRepository(AppDbContext _db)
        {
            db = _db;
        }

        public async Task<int> AddAsync(T entity)
        {
            await db.Set<T>().AddAsync(entity);
            
            return 1;

        }

        public async Task<int> DeleteAsync(int id)
        {
            var entity = await db.Set<T>().FirstOrDefaultAsync(e => e.Id == id);

            EntityEntry entityEntry = db.Entry<T>(entity);

            entityEntry.State = EntityState.Deleted;
            return entity.Id > 0 ? 1 : 0;

        }

        public async Task<IEnumerable<T>> GetAllAsync(int id)
        {
            IQueryable<T> query = db.Set<T>();

            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = db.Set<T>();

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.FirstOrDefaultAsync(a =>a.Id==id);
        }

        public async Task<int> UpdateAsync(int id, T entity)
        {
            var existingEntity = await db.Set<T>().FindAsync(id);

            if (existingEntity != null)
            {
                db.Entry(existingEntity).State = EntityState.Detached;
            }

            db.Entry(entity).State = EntityState.Modified;
            return entity.Id > 0 ? 1 : 0;

        }
    }
}
