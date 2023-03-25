using System.Linq.Expressions;
using DatabaseManager.EntityTemplate;

namespace DatabaseManager.ServiceTemplate;

public interface IService<TEntity> where TEntity : BaseEntity
{
	Task<IQueryable<TEntity>> ListAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
	Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
	Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
	Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
	Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
	Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
	Task RemoveAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : IEquatable<TId>;
	Task RemoveRangeAsync<TId>(IEnumerable<TId> ids, CancellationToken cancellationToken = default) where TId : IEquatable<TId>;
}