namespace DatabaseManager.EntityTemplate;

public abstract class BaseEntity<TId>
{
#pragma warning disable CS8618
	public TId Id { get; set; }
#pragma warning restore CS8618
}