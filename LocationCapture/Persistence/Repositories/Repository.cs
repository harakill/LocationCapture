using LocationCapture.Core;
using LocationCapture.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LocationCapture.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext Context;
        private DbSet<TEntity> _entities;

        public Repository(ApplicationDbContext context)
        {
            Context = context;
            _entities = Context.Set<TEntity>();
        }

        public async Task Add(TEntity entity)
        {
            await _entities.AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public async Task<TEntity> Get(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        public void Remove(TEntity entity)
        {
            _entities.Remove(entity);
        }
    }
}
