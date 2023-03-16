using System.ComponentModel.DataAnnotations;

namespace DatabaseManager.BaseEntityTemplate;

public abstract class BaseEntity
{
	[Key] public object Id { get; set; }
}