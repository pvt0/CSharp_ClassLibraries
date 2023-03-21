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

	public IEnumerable<TEntity> List(Expression<Func<TEntity, bool>>? expression = null)
	{
		return _unitOfWork.Repository<TEntity>().List(expression);
	}

	public IEnumerable<TEntity> List<TId>(IEnumerable<TId>? entityIds = null)
	{
		return _unitOfWork.Repository<TEntity>().List(entityIds);
	}

	public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
	{
		return await _unitOfWork.Repository<TEntity>().GetAsync(expression, cancellationToken);
	}

	public async Task<TEntity?> GetAsync<TId>(TId id, CancellationToken cancellationToken)
	{
		return await _unitOfWork.Repository<TEntity>().GetAsync(id, cancellationToken);
	}

	public async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken)
	{
		var result = await _unitOfWork.Repository<TEntity>().InsertAsync(entity, cancellationToken);
		await _unitOfWork.SaveAsync();
		return result;
	}

	public async Task InsertRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
	{
		await _unitOfWork.Repository<TEntity>().InsertRangeAsync(entities, cancellationToken);
		await _unitOfWork.SaveAsync();
	}

	public async Task<TEntity> UpdateAsync(TEntity entity)
	{
		var result = _unitOfWork.Repository<TEntity>().Update(entity);
		await _unitOfWork.SaveAsync();
		return result;
	}

	public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
	{
		_unitOfWork.Repository<TEntity>().UpdateRange(entities);
		await _unitOfWork.SaveAsync();
	}

	public async Task DeleteAsync(TEntity entity)
	{
		_unitOfWork.Repository<TEntity>().Delete(entity);
		await _unitOfWork.SaveAsync();
	}

	public async Task DeleteAsync<TId>(TId id, CancellationToken cancellationToken)
	{
		await _unitOfWork.Repository<TEntity>().DeleteAsync(id, cancellationToken);
		await _unitOfWork.SaveAsync();
	}

	public async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
	{
		_unitOfWork.Repository<TEntity>().DeleteRange(entities);
		await _unitOfWork.SaveAsync();
	}

	public async Task DeleteRangeAsync<TId>(IEnumerable<TId> ids)
	{
		_unitOfWork.Repository<TEntity>().DeleteRange(ids);
		await _unitOfWork.SaveAsync();
	}

	#endregion
}