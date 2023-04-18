using System.Runtime.CompilerServices;
// ReSharper disable PossibleMultipleEnumeration

namespace Bogoware.Monads;

public static class OptionalExtensions
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Optional<T> ToOptional<T>(this IEnumerable<T>? values) where T : class
		=> values is not null && values.Any()
			? Prelude.Some(values.First())
			: Prelude.None<T>();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Optional<T> Where<T>(this T? obj, Func<T, bool> predicate) where T : class 
		=> obj is not null && predicate(obj) 
			? Prelude.Some(obj) 
			: Prelude.None<T>();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Optional<T> WhereNot<T>(this T? obj, Func<T, bool> predicate) where T : class 
		=> obj is not null && !predicate(obj) 
			? Prelude.Some(obj) 
			: Prelude.None<T>();
}