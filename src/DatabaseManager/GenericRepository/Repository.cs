using System.Linq.Expressions;
using DatabaseManager.EntityTemplate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DatabaseManager.GenericRepository;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
	#region variables

	private readonly DbSet<TEntity> _dbSet;

	#endregion

	#region constructors

	public Repository(DbContext context)
	{
		_dbSet = context.Set<TEntity>();
	}

	#endregion

	#region IRepository implementation

	public Task<IQueryable<TEntity>> ListAsync(Expression<Func<TEntity, bool>>? expression = null, 
		CancellationToken cancellationToken = default)
		=> Task.Run(() => expression == null ? _dbSet : _dbSet.Where(expression), cancellationToken);

	public Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, 
		CancellationToken cancellationToken = default)
		=> _dbSet.FirstOrDefaultAsync(expression, cancellationToken: cancellationToken);

	public ValueTask<EntityEntry<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
		=> _dbSet.AddAsync(entity, cancellationToken);
	
	public Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
		=> _dbSet.AddRangeAsync(entities, cancellationToken);

	public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default) 
		=> Task.Run(() => _dbSet.Update(entity).Entity, cancellationToken);

	public Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
		=> Task.Run(() => _dbSet.UpdateRange(entities), cancellationToken);

	public Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default)
		=> Task.Run(() => _dbSet.Remove(entity), cancellationToken);

	public Task RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
		=> Task.Run(() => _dbSet.RemoveRange(entities), cancellationToken);

	#endregion
}