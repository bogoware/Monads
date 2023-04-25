// ReSharper disable UnusedMember.Global
namespace Bogoware.Monads;

internal static class TypeHelper
{
	public static string GetFriendlyTypeName(this Type type)
	{
		ArgumentNullException.ThrowIfNull(type);
		if (!type.IsGenericType) return type.Name;
		var genericTypes = string.Join(",",
			type.GetGenericArguments().Select(t => t.GetFriendlyTypeName()).ToArray());
		return $"{type.Name.Remove(type.Name.IndexOf('`'))}<{genericTypes}>";

	}

	public static string GetFriendlyTypeName(this object @object)
	{
		return @object.GetType().GetFriendlyTypeName();
	}
}