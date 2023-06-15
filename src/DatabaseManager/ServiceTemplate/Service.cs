using System.Linq.Expressions;
using DatabaseManager.EntityTemplate;
using DatabaseManager.GenericUnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace DatabaseManager.ServiceTemplate;

public class Service<TEntity, TId> where TEntity : BaseEntity<TId>
{
	#region variables

	private readonly IUnitOfWork _unitOfWork;

	#endregion

	#region constructors

	public Service(IServiceProvider provider)
	{
		var scoped = provider.CreateScope();
		_unitOfWork = scoped.ServiceProvider.GetRequiredService<IUnitOfWork>();
	}

	#endregion

	#region IService implementation

	protected async Task<IQueryable<TEntity>> ListAsync(Expression<Func<TEntity, bool>> expression,
		CancellationToken cancellationToken = default)
		=> await _unitOfWork.Repository<TEntity, TId>().ListAsync(expression, cancellationToken);

	protected async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, 
		CancellationToken cancellationToken = default)
		=> await _unitOfWork.Repository<TEntity, TId>().GetAsync(expression, cancellationToken);

	protected async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
	{
		var result = await _unitOfWork.Repository<TEntity, TId>().AddAsync(entity, cancellationToken);
		await _unitOfWork.SaveAsync(cancellationToken);
		return result.Entity;
	}

	protected async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
	{
		await _unitOfWork.Repository<TEntity, TId>().AddRangeAsync(entities, cancellationToken);
		await _unitOfWork.SaveAsync(cancellationToken);
	}

	protected async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
	{
		var result = await _unitOfWork.Repository<TEntity, TId>().UpdateAsync(entity, cancellationToken);
		await _unitOfWork.SaveAsync(cancellationToken);
		return result;
	}

	protected async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
	{
		await _unitOfWork.Repository<TEntity, TId>().UpdateRangeAsync(entities, cancellationToken);
		await _unitOfWork.SaveAsync(cancellationToken);
	}

	protected async Task RemoveAsync(TId id, CancellationToken cancellationToken = default)
	{
		var entity = await _unitOfWork.Repository<TEntity, TId>()
			.GetAsync(entity => entity.Id!.Equals(id), cancellationToken);

		if (entity == null) 
			throw new ArgumentException($"Does not exist '{nameof(TEntity)}' with Id {id}");
		
		await _unitOfWork.Repository<TEntity, TId>().RemoveAsync(entity, cancellationToken);
		await _unitOfWork.SaveAsync(cancellationToken);
	}

	protected async Task RemoveRangeAsync(IEnumerable<TId> ids, CancellationToken cancellationToken = default)
	{
		var entities = await _unitOfWork.Repository<TEntity, TId>()
			.ListAsync(entity => ids.Contains(entity.Id), cancellationToken);

		if (!entities.Any())
			throw new ArgumentException($"Does not exists '{nameof(TEntity)}' with Ids: {ids}");
		
		await _unitOfWork.Repository<TEntity, TId>().RemoveRangeAsync(entities, cancellationToken);
		await _unitOfWork.SaveAsync(cancellationToken);
	}

	#endregion
}