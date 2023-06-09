using DatabaseManager.EntityTemplate;
using DatabaseManager.GenericRepository;
using Microsoft.EntityFrameworkCore.Storage;

namespace DatabaseManager.GenericUnitOfWork;

internal interface IUnitOfWork : IDisposable
{
	IRepository<TEntity, TId> Repository<TEntity, TId>() where TEntity : BaseEntity<TId>;
	IDbContextTransaction BeginTransaction();
	Task<int> SaveAsync(CancellationToken cancellationToken);
}