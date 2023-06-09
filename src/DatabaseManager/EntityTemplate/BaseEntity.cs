using System.ComponentModel.DataAnnotations;

namespace DatabaseManager.EntityTemplate;

public abstract class BaseEntity<TId>
{
#pragma warning disable CS8618
	[Key] public TId Id { get; set; }
#pragma warning restore CS8618
}