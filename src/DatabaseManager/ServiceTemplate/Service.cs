using System.Linq.Expressions;
using DatabaseManager.EntityTemplate;
using DatabaseManager.GenericUnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace DatabaseManager.ServiceTemplate;

public class Service<TEntity> : IService<TEntity> where TEntity : BaseEntity
{
	#region variables

	private readonly IUnitOfWork _unitOfWork;

	#endregion

	#region constructors

	protected Service(IServiceProvider provider)
	{
		var scoped = provider.CreateScope();
		_unitOfWork = scoped.ServiceProvider.GetRequiredService<IUnitOfWork>();
	}

	#endregion

	#region IService implementation

	public async Task<IQueryable<TEntity>> ListAsync(Expression<Func<TEntity, bool>> expression,
		CancellationToken cancellationToken = default)
		=> await _unitOfWork.Repository<TEntity>().ListAsync(expression, cancellationToken);

	public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, 
		CancellationToken cancellationToken = default)
		=> await _unitOfWork.Repository<TEntity>().GetAsync(expression, cancellationToken);

	public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
	{
		var result = await _unitOfWork.Repository<TEntity>().AddAsync(entity, cancellationToken);
		await _unitOfWork.SaveAsync(cancellationToken);
		return result.Entity;
	}

	public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
	{
		await _unitOfWork.Repository<TEntity>().AddRangeAsync(entities, cancellationToken);
		await _unitOfWork.SaveAsync(cancellationToken);
	}

	public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
	{
		var result = await _unitOfWork.Repository<TEntity>().UpdateAsync(entity, cancellationToken);
		await _unitOfWork.SaveAsync(cancellationToken);
		return result;
	}

	public async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
	{
		await _unitOfWork.Repository<TEntity>().UpdateRangeAsync(entities, cancellationToken);
		await _unitOfWork.SaveAsync(cancellationToken);
	}

	public async Task RemoveAsync<TId>(TId id, CancellationToken cancellationToken = default)
	{
		var entity = await _unitOfWork.Repository<TEntity>()
			.GetAsync(entity => entity.Id == (object)id, cancellationToken);

		if (entity == null) 
			throw new NullReferenceException($"Does not exist '{nameof(TEntity)}' with Id {id}");
		
		await _unitOfWork.Repository<TEntity>().RemoveAsync(entity, cancellationToken);
		await _unitOfWork.SaveAsync(cancellationToken);
	}

	public async Task RemoveRangeAsync<TId>(IEnumerable<TId> ids, CancellationToken cancellationToken = default)
	{
		var entities = await _unitOfWork.Repository<TEntity>()
			.ListAsync(entity => ids.Contains((TId)entity.Id), cancellationToken);

		if (!entities.Any())
			throw new NullReferenceException();
		
		await _unitOfWork.Repository<TEntity>().RemoveRangeAsync(entities, cancellationToken);
		await _unitOfWork.SaveAsync(cancellationToken);
	}

	#endregion
}