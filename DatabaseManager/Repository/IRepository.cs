using System.Linq.Expressions;
using DatabaseManager.Entities;
using Paging.Collection;

namespace DatabaseManager.Repository;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
	Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression);
	IEnumerable<TEntity> List<TId>(IEnumerable<TId>? entity = null);
	Task<TEntity?> GetByIdAsync<TId>(TId id);
	TEntity Insert(TEntity entity);
	void InsertRange(IEnumerable<TEntity> entities);
	TEntity Update(TEntity entity);
	void Delete(TEntity entity);
	void Delete<TId>(TId id);
	void DeleteRange(IEnumerable<TEntity> entities);
	void DeleteByIdRange<TId>(IEnumerable<TId> ids);
}