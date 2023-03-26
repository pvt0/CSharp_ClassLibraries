using Microsoft.EntityFrameworkCore;

namespace DatabaseManager.IntegrationTest.Configurations.Domain;

public class DbContextTest : DbContext
{
	public virtual DbSet<TestEntity> TestEntities { get; set; }

	public DbContextTest(DbContextOptions options) : base(options)
	{
		
	}
}