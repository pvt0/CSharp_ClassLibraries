using System.Linq.Expressions;
using DatabaseManager.Entities;

namespace DatabaseManager.Repository;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
	Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);
	IEnumerable<TEntity> List<TId>(IEnumerable<TId>? entity = null);
	Task<TEntity?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken);
	Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken);
	Task InsertRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
	TEntity Update(TEntity entity);
	void UpdateRange(IEnumerable<TEntity> entities);
	void Delete(TEntity entity);
	Task DeleteAsync<TId>(TId id, CancellationToken cancellationToken);
	void DeleteRange(IEnumerable<TEntity> entities);
	void DeleteByIdRange<TId>(IEnumerable<TId> ids);
}