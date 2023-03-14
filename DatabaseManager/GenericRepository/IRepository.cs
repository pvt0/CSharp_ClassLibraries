using System.Linq.Expressions;
using DatabaseManager.GenericBaseEntity;

namespace DatabaseManager.GenericRepository;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
	IEnumerable<TEntity> List(Expression<Func<TEntity, bool>>? expression = null);
	IEnumerable<TEntity> List<TId>(IEnumerable<TId>? entity = null);
	Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);
	Task<TEntity?> GetAsync<TId>(TId id, CancellationToken cancellationToken);
	Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken);
	Task InsertRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
	TEntity Update(TEntity entity);
	void UpdateRange(IEnumerable<TEntity> entities);
	void Delete(TEntity entity);
	Task DeleteAsync<TId>(TId id, CancellationToken cancellationToken);
	void DeleteRange(IEnumerable<TEntity> entities);
	void DeleteRange<TId>(IEnumerable<TId> ids);
}