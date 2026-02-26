using System.Linq.Expressions;
using Core.Abstractions;
using Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace System.Infrastructure.Implementations
{
    public class Repository<T>(DbContext Context) : IRepository<T>
        where T : BaseEntity
    {
        public async Task<bool> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await Context.AddAsync(entity, cancellationToken);
            return true;
        }

        public async Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            entity.DeActive();
            await Task.FromResult(Context.Update(entity));
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            T entity = await GetByIdAsync(id, cancellationToken);
            return await DeleteAsync(entity, cancellationToken);
        }

        public IQueryable<T> GetAllAsQueryable(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = Context.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query;
        }

        public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            await Context?.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        public async Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            await Task.FromResult(Context.Update(entity));
            return true;
        }
    }
}
