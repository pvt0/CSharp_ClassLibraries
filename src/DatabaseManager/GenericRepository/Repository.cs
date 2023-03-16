using System.Linq.Expressions;
using DatabaseManager.BaseEntityTemplate;
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

	public IEnumerable<TEntity> List(Expression<Func<TEntity, bool>>? expression = null)
		=> expression == null ? _dbSet : _dbSet.Where(expression);

	public IEnumerable<TEntity> List<TId>(IEnumerable<TId>? entity = null)
		=> _dbSet.Where(e => entity == null || entity.Contains((TId)e.Id));
	
	public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
		=> await _dbSet.SingleOrDefaultAsync(expression, cancellationToken: cancellationToken);

	public async Task<TEntity?> GetAsync<TId>(TId id, CancellationToken cancellationToken)
		=> await _dbSet.SingleOrDefaultAsync(e => e.Id == (object)id, cancellationToken: cancellationToken);

	public async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken)
	{
		var result = await _dbSet.AddAsync(entity, cancellationToken);
		return result.Entity;
	}

	public async Task InsertRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
		=> await _dbSet.AddRangeAsync(entities, cancellationToken);

	public TEntity Update(TEntity entity)
	{
		var result = _dbSet.Update(entity);
		return result.Entity;
	}

	public void UpdateRange(IEnumerable<TEntity> entities)
		=> _dbSet.UpdateRange(entities);

	public void Delete(TEntity entity)
		=> _dbSet.Remove(entity);

	public async Task DeleteAsync<TId>(TId id, CancellationToken cancellationToken)
	{
		var entity = await _dbSet.FindAsync(id, cancellationToken);
		
		if (entity == null)
			throw new Exception($"Does not exist {nameof(entity)} with id {id}");
		
		Delete(entity);
	}

	public void DeleteRange(IEnumerable<TEntity> entities)
		=> _dbSet.RemoveRange(entities);

	public void DeleteRange<TId>(IEnumerable<TId> ids)
	{
		var entities = List(ids);
	
		DeleteRange(entities);
	}

	#endregion
}