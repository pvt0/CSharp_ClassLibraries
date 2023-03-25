using DatabaseManager.EntityTemplate;
using DatabaseManager.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace DatabaseManager.GenericUnitOfWork;

public class UnitOfWork : IUnitOfWork
{
	#region variables

	private readonly DbContext _context;
	private readonly Dictionary<string, object> _repositories;
//	private readonly IDbContextTransaction _transaction;

	#endregion

	#region constructors

	public UnitOfWork(DbContext context)
	{
		_context = context;
		_repositories = new Dictionary<string, object>();
	}

	#endregion

	#region IUnitOfWork implementation

	public IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
	{
		var type = typeof(TEntity);

		if ( ! _repositories.ContainsKey(type.Name))
		{
			var repoType = typeof(Repository<>).MakeGenericType(type);
			var repo = Activator.CreateInstance(repoType, _context);

			if (repo != null)
				_repositories.Add(type.Name, repo);
		}

		return (IRepository<TEntity>)_repositories[type.Name];
	}

	public async Task<int> SaveAsync(CancellationToken cancellationToken)
		=> await _context.SaveChangesAsync(cancellationToken);

	#endregion

	#region IDispose implementation

	public void Dispose()
		=> _context.Dispose();

	#endregion
}