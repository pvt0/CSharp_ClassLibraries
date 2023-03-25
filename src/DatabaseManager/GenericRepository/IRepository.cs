using System.Linq.Expressions;
using DatabaseManager.EntityTemplate;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DatabaseManager.GenericRepository;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
	Task<IQueryable<TEntity>> ListAsync(Expression<Func<TEntity, bool>>? expression = null, CancellationToken cancellationToken = default);
	Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
	ValueTask<EntityEntry<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
	Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
	Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
	Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
	Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default);
	Task RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
}