using Core.Persistence.Repositories.Interface;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.Persistence.Repositories.Abstracts
{
    public class ReadRepository<TEntity, TContext> : IReadRepository<TEntity>
       where TEntity : BaseEntity
       where TContext : DbContext
    {
        protected TContext Context;

        public ReadRepository(TContext context)
        {
            Context = context;
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return await Context.Set<TEntity>().AnyAsync(predicate);
        }

        public async Task<int> CountAsync()
        {
            return await Context.Set<TEntity>().CountAsync();
        }
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> condition)
        {
            return await Context.Set<TEntity>().Where(condition).CountAsync();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate = null)
        {
            return Context.Set<TEntity>().FirstOrDefault(predicate);
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
                return Context.Set<TEntity>().AsQueryable();
            else
                return Context.Set<TEntity>().Where(predicate);
        }

        public IQueryable<TEntity> GetAllFiles(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.AsNoTracking();
        }
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return await Context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }



        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
                return Context.Set<TEntity>().ToList();
            else
                return await Context.Set<TEntity>().Where(predicate).ToListAsync();
        }


    }
}

