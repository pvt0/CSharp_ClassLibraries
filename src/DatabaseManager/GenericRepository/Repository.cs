using System.Linq.Expressions;
using DatabaseManager.EntityTemplate;
using Microsoft.EntityFrameworkCore;

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

	public Task<IQueryable<TEntity>> ListAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
		=> Task.Run(() => _dbSet.Where(expression), cancellationToken);

	public Task<IQueryable<TEntity>> ListAsync<TId>(IEnumerable<TId>? ids = null, CancellationToken cancellationToken = default)
		=> Task.Run(() => _dbSet.Where(e => ids == null || ids.Contains((TId)e.Id)), cancellationToken);
	
	public Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
		=> _dbSet.SingleOrDefaultAsync(expression, cancellationToken: cancellationToken);

	public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
		=> (await _dbSet.AddAsync(entity, cancellationToken)).Entity;
	
	public Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
		=> _dbSet.AddRangeAsync(entities, cancellationToken);

	public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default) 
		=> Task.Run(() => _dbSet.Update(entity).Entity, cancellationToken);

	public Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
		=> Task.Run(() => _dbSet.UpdateRange(entities), cancellationToken);

	public async Task DeleteAsync<TId>(TId id, CancellationToken cancellationToken = default)
	{
		var entity = await _dbSet.FindAsync(id, cancellationToken);
		
		if (entity == null)
			throw new Exception($"Does not exist {nameof(entity)} with id {id}");
		
		_dbSet.Remove(entity);
	}

	public async Task DeleteRangeAsync<TId>(IEnumerable<TId> ids, CancellationToken cancellationToken = default)
	{
		var entities = await ListAsync(ids, cancellationToken);
		_dbSet.RemoveRange(entities);
	}

	#endregion
}