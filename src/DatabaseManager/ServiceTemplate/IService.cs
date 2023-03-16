using System.Linq.Expressions;
using DatabaseManager.EntityTemplate;

namespace DatabaseManager.ServiceTemplate;

public interface IService<TEntity> where TEntity : BaseEntity
{
	IEnumerable<TEntity> List(Expression<Func<TEntity, bool>>? expression = null);
	IEnumerable<TEntity> List<TId>(IEnumerable<TId>? entity = null);
	Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);
	Task<TEntity?> GetAsync<TId>(TId id, CancellationToken cancellationToken);
	Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken);
	Task InsertRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
	Task<TEntity> UpdateAsync(TEntity entity);
	Task UpdateRangeAsync(IEnumerable<TEntity> entities);
	Task DeleteAsync(TEntity entity);
	Task DeleteAsync<TId>(TId id, CancellationToken cancellationToken);
	Task DeleteRangeAsync(IEnumerable<TEntity> entities);
	Task DeleteRangeAsync<TId>(IEnumerable<TId> ids);
}