using System.Linq.Expressions;
using DatabaseManager.EntityTemplate;

namespace DatabaseManager.ServiceTemplate;

public interface IService<TEntity> where TEntity : BaseEntity
{
	Task<IQueryable<TEntity>> ListAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
	Task<IQueryable<TEntity>> ListAsync<TId>(IEnumerable<TId>? ids = null,
		CancellationToken cancellationToken = default);
	Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
	Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
	Task InsertRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
	Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
	Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
	Task DeleteAsync<TId>(TId id, CancellationToken cancellationToken = default);
	Task DeleteRangeAsync<TId>(IEnumerable<TId> ids, CancellationToken cancellationToken = default);
}