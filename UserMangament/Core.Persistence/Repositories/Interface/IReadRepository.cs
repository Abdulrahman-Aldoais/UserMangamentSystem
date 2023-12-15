using Domain.Entities;
using System.Linq.Expressions;

namespace Core.Persistence.Repositories.Interface
{
    public interface IReadRepository<T> where T : BaseEntity
    {
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate = null);
        T Get(Expression<Func<T, bool>> predicate = null);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate = null);
        IQueryable<T> GetAllFiles(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null);
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> predicate = null);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> condition);




    }
}
