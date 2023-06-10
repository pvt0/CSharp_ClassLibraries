using DatabaseManager.GenericRepository;
using DatabaseManager.GenericUnitOfWork;
using DatabaseManager.ServiceTemplate;
using Microsoft.Extensions.DependencyInjection;

namespace DatabaseManager;

public static class DependencyInjection
{
	public static IServiceCollection AddDatabaseManagerServices(this IServiceCollection services)
	{
		if (services == null)
			throw new ArgumentNullException(nameof(services));
		
		services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
		services.AddScoped<IUnitOfWork, UnitOfWork>();

		return services;
	}
}