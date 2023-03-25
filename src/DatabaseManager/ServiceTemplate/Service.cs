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
	{
		return await _unitOfWork.Repository<TEntity>().ListAsync(expression, cancellationToken);
	}

	public async Task<IQueryable<TEntity>> ListAsync<TId>(IEnumerable<TId>? ids = null,
		CancellationToken cancellationToken = default)
	{
		return await _unitOfWork.Repository<TEntity>().ListAsync(ids, cancellationToken);
	}

	public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, 
		CancellationToken cancellationToken = default)
	{
		return await _unitOfWork.Repository<TEntity>().GetAsync(expression, cancellationToken);
	}

	public async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
	{
		var result = await _unitOfWork.Repository<TEntity>().AddAsync(entity, cancellationToken);
		await _unitOfWork.SaveAsync(cancellationToken);
		return result;
	}

	public async Task InsertRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
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

	public async Task DeleteAsync<TId>(TId id, CancellationToken cancellationToken = default)
	{
		await _unitOfWork.Repository<TEntity>().DeleteAsync(id, cancellationToken);
		await _unitOfWork.SaveAsync(cancellationToken);
	}

	public async Task DeleteRangeAsync<TId>(IEnumerable<TId> ids, CancellationToken cancellationToken = default)
	{
		await _unitOfWork.Repository<TEntity>().DeleteRangeAsync(ids, cancellationToken);
		await _unitOfWork.SaveAsync(cancellationToken);
	}

	#endregion
}