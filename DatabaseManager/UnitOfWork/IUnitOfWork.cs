using DatabaseManager.Entities;
using DatabaseManager.Repository;

namespace DatabaseManager.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
	IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
/*	
	void BeginTransaction();
	void Rollback();
	bool Commit();
*/
	Task<int> SaveAsync();
}