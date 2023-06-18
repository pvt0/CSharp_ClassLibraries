using DatabaseManager.EntityTemplate;
using DatabaseManager.GenericRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DatabaseManager.GenericUnitOfWork;

internal class UnitOfWork : IUnitOfWork
{
	#region variables

	private readonly DbContext _context;
	private readonly Dictionary<string, object> _repositories;

	#endregion

	#region constructors

	public UnitOfWork(DbContext context)
	{
		_context = context;
		_repositories = new Dictionary<string, object>();
	}

	#endregion

	#region IUnitOfWork implementation

	public IRepository<TEntity, TId> Repository<TEntity, TId>() where TEntity : BaseEntity<TId>
	{
		var entityType = typeof(TEntity);
		var idType = typeof(TId);

		if ( ! _repositories.ContainsKey(entityType.Name))
		{
			var repoType = typeof(Repository<,>).MakeGenericType(entityType, idType);
			var repo = Activator.CreateInstance(repoType, _context);

			if (repo != null)
				_repositories.Add(entityType.Name, repo);
		}

		return (IRepository<TEntity, TId>)_repositories[entityType.Name];
	}

	public IDbContextTransaction BeginTransaction()
		=> _context.Database.BeginTransaction();


	public async Task<int> SaveAsync(CancellationToken cancellationToken)
		=> await _context.SaveChangesAsync(cancellationToken);

	#endregion

	#region IDispose implementation

	public void Dispose()
	{
		_context.Dispose();
	}

	#endregion
}