using System.Linq.Expressions;
using Core.Entity;

namespace Core.Abstractions
{
    public interface IRepository<T>
        where T : BaseEntity
    {
        IQueryable<T> GetAllAsQueryable(Expression<Func<T, bool>> filter = null);
        Task<bool> AddAsync(T entity, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
