using DatabaseManager.EntityTemplate;
using DatabaseManager.GenericRepository;
using Microsoft.EntityFrameworkCore.Storage;

namespace DatabaseManager.GenericUnitOfWork;

public interface IUnitOfWork : IDisposable
{
	IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
	IDbContextTransaction BeginTransaction();
	Task<int> SaveAsync(CancellationToken cancellationToken);
}