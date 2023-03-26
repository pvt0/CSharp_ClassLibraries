using System.ComponentModel.DataAnnotations;

namespace DatabaseManager.EntityTemplate;

public abstract class BaseEntity<TId>
{
	[Key] public TId Id { get; set; }
}