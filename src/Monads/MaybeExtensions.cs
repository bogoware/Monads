using System.Runtime.CompilerServices;
// ReSharper disable PossibleMultipleEnumeration

namespace Bogoware.Monads;

public static class MaybeExtensions
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Maybe<T> ToMaybe<T>(this IEnumerable<T>? values) where T : class
		=> values is not null && values.Any()
			? Prelude.Some(values.First())
			: Prelude.None<T>();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Maybe<T> Where<T>(this T? obj, Func<T, bool> predicate) where T : class 
		=> obj is not null && predicate(obj) 
			? Prelude.Some(obj) 
			: Prelude.None<T>();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Maybe<T> WhereNot<T>(this T? obj, Func<T, bool> predicate) where T : class 
		=> obj is not null && !predicate(obj) 
			? Prelude.Some(obj) 
			: Prelude.None<T>();
}