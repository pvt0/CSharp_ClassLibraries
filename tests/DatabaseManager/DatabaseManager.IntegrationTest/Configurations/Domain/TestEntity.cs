using DatabaseManager.EntityTemplate;

namespace DatabaseManager.IntegrationTest.Configurations.Domain;

public class TestEntity : BaseEntity<Guid>
{
	public string? Name { get; set; }
	public int Data { get; set; }
}