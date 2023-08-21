using Microsoft.EntityFrameworkCore;
using project.Entity;

namespace project.Generics
{
    public class GenericService<TEntity, TContext> : IGenericService<TEntity>
        where TEntity : class, IEntity
        where TContext : DbContext
    {

        private readonly TContext _context;

        public GenericService(TContext context) => _context = context;

        public async Task<List<TEntity>> GetAll() => await _context.Set<TEntity>().ToListAsync();

        public async Task<TEntity> Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Delete(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null) return entity;

            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity?> Get(int id) => await _context.Set<TEntity>().FindAsync(id);
    }
}
