using System.Linq.Expressions;
using DatabaseManager.EntityTemplate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DatabaseManager.GenericRepository;

internal class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : BaseEntity<TId>
{
	#region variables

	private readonly DbSet<TEntity> _dbSet;
	private readonly DbContext _context;

	#endregion

	#region constructors

	public Repository(DbContext context)
	{
		_context = context;
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

	public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default) 
	{
		var entry = _dbSet.Attach(entity);
		entry.State = EntityState.Modified;
		await _context.SaveChangesAsync(cancellationToken);
		return entry.Entity;
	}

	public async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
	{
		_dbSet.AttachRange(entities);
		foreach (var entity in entities)
		{
			_context.Entry(entity).State = EntityState.Modified;
		}
		await _context.SaveChangesAsync(cancellationToken);
	}


	public Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default)
		=> Task.Run(() => _dbSet.Remove(entity), cancellationToken);

	public Task RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
		=> Task.Run(() => _dbSet.RemoveRange(entities), cancellationToken);

	#endregion
}