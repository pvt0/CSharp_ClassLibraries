using System.ComponentModel.DataAnnotations;

namespace DatabaseManager.EntityTemplate;

public abstract class BaseEntity
{
	[Key] public object Id { get; set; }
}