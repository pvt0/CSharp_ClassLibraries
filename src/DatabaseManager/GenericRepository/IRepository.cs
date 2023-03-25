using System.Linq.Expressions;
using DatabaseManager.EntityTemplate;

namespace DatabaseManager.GenericRepository;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
	Task<IQueryable<TEntity>> ListAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
	Task<IQueryable<TEntity>> ListAsync<TId>(IEnumerable<TId>? ids = null, CancellationToken cancellationToken = default);
	Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
	Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
	Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
	Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
	Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
	Task DeleteAsync<TId>(TId id, CancellationToken cancellationToken = default);
	Task DeleteRangeAsync<TId>(IEnumerable<TId> ids, CancellationToken cancellationToken = default);
}