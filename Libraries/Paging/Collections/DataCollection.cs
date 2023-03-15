namespace Paging.Collection;

public class DataCollection<T>
{
	#region properties

	public bool HasItems => Items != null && Items.Any();

	public IEnumerable<T>? Items { get; set; }
	public int Total { get; set; }
	public int Page { get; set; }
	public int Pages { get; set; }

	#endregion
}