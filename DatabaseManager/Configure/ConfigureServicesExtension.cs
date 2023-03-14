using DatabaseManager.Repository;
using DatabaseManager.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace DatabaseManager.Configure;

public static class ConfigureServicesExtension
{
	public static IServiceCollection AddDatabaseManagerServices(this IServiceCollection services)
	{
		if (services == null)
			throw new ArgumentNullException(nameof(services));
		
		services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
		services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

		return services;
	}
}