using DatabaseManager.GenericRepository;
using DatabaseManager.IntegrationTest.Configurations;
using DatabaseManager.IntegrationTest.Configurations.Domain;
using Microsoft.EntityFrameworkCore;

namespace DatabaseManager.IntegrationTest;

public class RepositoryTests
{
	private readonly DbContextTest _db;
	private readonly IRepository<TestEntity, Guid> _repo;


	public RepositoryTests()
	{
		var options = new DbContextOptionsBuilder().UseInMemoryDatabase("RepoTestDb").Options;
		_db = new DbContextTest(options);
		_repo = new Repository<TestEntity, Guid>(_db);
		
		var random = new Random();

		for (int i = 0; i < 5; i++)
		{
			_db.TestEntities.Add(new TestEntity
			{
				Id = Guid.NewGuid(),
				Name = $"Entity{i+1}",
				Data = random.Next()
			});
		}

		_db.SaveChanges();
	}

	[Fact]
	public async Task ListAsync_ExpressionEqualNull_ReturnListWith5Items()
	{
		// Act
		var result = await _repo.ListAsync();
		
		// Assert
		Assert.IsAssignableFrom<IQueryable<TestEntity>>(result);
		Assert.True(result.Any());
	}
	
	[Fact]
	public async Task ListAsync_ExpressionNotNull_ReturnListWith5Items()
	{
		// Act
		var result = await _repo.ListAsync(entity => entity.Data > 700);
		
		// Assert
		Assert.IsAssignableFrom<IQueryable<TestEntity>>(result);
		Assert.True(result.Any());
	}

	[Fact]
	public async Task GetAsync_ByName_Entity4()
	{
		// Act
		var result = await _repo.GetAsync(e => e.Name == "Entity4");
		
		// Assert
		Assert.True(result != null);
		Assert.IsType<TestEntity?>(result);
		Assert.True(result.Name == "Entity4");
	}

    [Fact]
    public async Task GetAsync_ById_Error()
	{
		// Act
		var result = await _repo.GetAsync(e => e.Id == Guid.NewGuid());
		
		// Assert
		Assert.True(result == null);
	}

    [Fact]
    public async Task AddAsync_Correctly()
    {
	    // Arrange
	    var newEntity = new TestEntity()
	    {
		    Data = 435343,
		    Name = "manual added"
	    };
	    
	    // Act
	    var result = await _repo.AddAsync(newEntity);
	    await _db.SaveChangesAsync();

	    // Assert
	    Assert.IsType<TestEntity>(result.Entity);
	    Assert.True(result.Entity.Name == newEntity.Name && result.Entity.Data == newEntity.Data);
	    Assert.IsType<Guid>(result.Entity.Id);
	    Assert.True(result.Entity.Id == newEntity.Id);
    }
    
    [Fact]
    public async Task AddAsync_WithAnExistedId_DontAdded()
    {
	    // Arrange
	    var newEntity = new TestEntity()
	    {
		    Data = 768876,
		    Name = "throw exception",
		    Id = new Guid("410672ac-dc14-4a32-94f9-8353d3d49d4b")
	    };
	    await _db.TestEntities.AddAsync(newEntity);
	    newEntity.Data = 435343;
	    newEntity.Name = "manual added";
	    
	    // Act
	    var result = await _repo.AddAsync(newEntity);
	    await _db.SaveChangesAsync();
	    var count = _db.TestEntities.Count(e => e.Id == newEntity.Id);

	    // Assert
	    Assert.IsType<TestEntity>(result.Entity);
	    Assert.True(result.Entity.Name == newEntity.Name && result.Entity.Data == newEntity.Data);
	    Assert.IsType<Guid>(result.Entity.Id);
	    Assert.True(result.Entity.Id == newEntity.Id);
	    Assert.True(count == 1);
    }
}