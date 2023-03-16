using DatabaseManager.BaseEntityTemplate;
using DatabaseManager.GenericRepository;

namespace DatabaseManager.GenericUnitOfWork;

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