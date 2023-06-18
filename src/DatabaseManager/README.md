# Database Manager

Develop a CRUD functionality leveraging a repository pattern and Entity Framework Core.


## Installation

Using the NuGet package manager console within Visual Studio run the following command:

```
Install-Package DatabaseManagerInfrastructure
```

Or using the .NET Core CLI from a terminal window:

```
dotnet add package DatabaseManagerInfrastructure
```

## Creating a new entity

To create an entity, it must inherit from the `BaseEntity<TId>` class, where `TId` is the data type of the identifier in the database (Key).

``` c#
public class MyEntity : BaseEntity<Guid>
{
	public string Name { get; set; }
	public int Age { get; set; }
}
```

After creating the necessary entities you should create the `DbContext`.

## Creating Service

To create a new service, it is recommended to derive from the `Service<TEntity, TId>` class and implement the necessary functions by leveraging the protected methods inherited from this class.


| Method             | Parameters                                                                                | Description                                                                                                                                     |
| ------------------ | ----------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------- |
| `ListAsync`        | `expression`: `Expression<Func<TEntity, bool>>`, `cancellationToken`: `CancellationToken` | Retrieves a list of entities of type `TEntity` that satisfy the specified expression asynchronously.                                            |
| `GetAsync`         | `expression`: `Expression<Func<TEntity, bool>>`, `cancellationToken`: `CancellationToken` | Retrieves a single entity of type `TEntity` that satisfies the specified expression asynchronously.                                             |
| `AddAsync`         | `entity`: `TEntity`, `cancellationToken`: `CancellationToken`                             | Adds a new entity of type `TEntity` asynchronously and returns the added entity.                                                                |
| `AddRangeAsync`    | `entities`: `IEnumerable<TEntity>`, `cancellationToken`: `CancellationToken`              | Adds a range of entities of type `TEntity` asynchronously.                                                                                      |
| `UpdateAsync`      | `entity`: `TEntity`, `cancellationToken`: `CancellationToken`                             | Updates an existing entity of type `TEntity` asynchronously and returns the updated entity.                                                     |
| `UpdateRangeAsync` | `entities`: `IEnumerable<TEntity>`, `cancellationToken`: `CancellationToken`              | Updates a range of entities of type `TEntity` asynchronously.                                                                                   |
| `RemoveAsync`      | `entity`: `TEntity`, `cancellationToken`: `CancellationToken`                             | Removes the specified entity of type `TEntity` asynchronously.                                                                                  |
| `RemoveAsync`      | `id`: `TId`, `cancellationToken`: `CancellationToken`                                     | Removes an entity of type `TEntity` with the specified `id` asynchronously. Throws an exception if the entity does not exist.                   |
| `RemoveRangeAsync` | `entities`: `IEnumerable<TEntity>`, `cancellationToken`: `CancellationToken`              | Removes a range of entities of type `TEntity` asynchronously.                                                                                   |
| `RemoveRangeAsync` | `ids`: `IEnumerable<TId>`, `cancellationToken`: `CancellationToken`                       | Removes a range of entities of type `TEntity` with the specified `ids` asynchronously. Throws an exception if any of the entities do not exist. |

Example:

``` C#
public class MyService : Service<MyEntity, Guid>
{
	public MyService(IServiceProvider services) : base(services) { }

	public async Task<MyEntity> CreateMyEntity(MyEntity entity, CancellationToken cancellationToke = default) 
	{
		return await AddAsync(entity, cancellationToken);
	}

	// and more methods as needed
}
```