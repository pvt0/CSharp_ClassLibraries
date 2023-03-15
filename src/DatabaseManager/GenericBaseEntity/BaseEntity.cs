using System.ComponentModel.DataAnnotations;

namespace DatabaseManager.GenericBaseEntity;

public abstract class BaseEntity
{
	[Key] public object Id { get; set; }
}