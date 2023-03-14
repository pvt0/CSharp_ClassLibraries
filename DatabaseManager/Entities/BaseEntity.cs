using System.ComponentModel.DataAnnotations;

namespace DatabaseManager.Entities;

public abstract class BaseEntity
{
	[Key] public object Id { get; set; }
}