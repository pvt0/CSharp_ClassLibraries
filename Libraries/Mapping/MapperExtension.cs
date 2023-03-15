using System.Text.Json;

namespace Mapping;

public static class MapperExtension
{
	#region static methods

	public static T MapTo<T>(this object value) => 
		JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(value));

	#endregion
}