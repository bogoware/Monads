namespace Bogoware.Monads;

internal static class TypeHelper
{
	public static string GetFriendlyTypeName(this Type type)
	{
		ArgumentNullException.ThrowIfNull(type);
		if (type.IsGenericType)
		{
			var genericTypes = string.Join(",",
				type.GetGenericArguments().Select(t => t.GetFriendlyTypeName()).ToArray());
			return $"{type.Name.Remove(type.Name.IndexOf('`'))}<{genericTypes}>";
		}

		return type.Name;
	}

	public static string GetFriendlyTypeName(this object @object)
	{
		return @object.GetType().GetFriendlyTypeName();
	}
}