using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

// ReSharper disable MemberCanBePrivate.Global

namespace Bogoware.Monads;

public static class EnumerableMaybeExtensions
{
	/// <summary>
	/// Determines if all <see cref="Maybe{T}"/>s of a sequence are <c>Some</c>s.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool IsAllSome(this IEnumerable<IMaybe> maybes)
		=> maybes.All(_ => _.IsSome);

	/// <inheritdoc cref="IsAllSome"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool IsAllSome<T>(this IEnumerable<Maybe<T>> maybes) where T : class
		=> maybes.All(_ => _.IsSome);

	/// <summary>
	/// Determines if all <see cref="Maybe{T}"/>s of a sequence are <c>None</c>s.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool IsAllNone(this IEnumerable<IMaybe> maybes)
		=> maybes.All(_ => _.IsNone);

	/// <inheritdoc cref="IsAllNone"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool IsAllNone<T>(this IEnumerable<Maybe<T>> maybes) where T : class
		=> maybes.All(_ => _.IsNone);

	/// <summary>
	/// Determines if any <see cref="Maybe{T}"/> of a sequence is <c>Some</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool IsAnySome(this IEnumerable<IMaybe> maybes)
		=> maybes.Any(_ => _.IsSome);

	/// <inheritdoc cref="IsAnySome"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool IsAnySome<T>(this IEnumerable<Maybe<T>> maybes) where T : class
		=> maybes.Any(_ => _.IsSome);

	/// <summary>
	/// Determines if any <see cref="Maybe{T}"/> of a sequence is <c>None</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool IsAnyNone(this IEnumerable<IMaybe> maybes)
		=> maybes.Any(_ => _.IsNone);

	/// <inheritdoc cref="IsAnyNone"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool IsAnyNone<T>(this IEnumerable<Maybe<T>> maybes) where T : class => maybes.Any(_ => _.IsNone);

	/// <summary>
	/// Extract values from <see cref="Maybe{T}"/>s.
	/// <c>None</c>s are discarded.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IEnumerable<T> SelectValues<T>(this IEnumerable<Maybe<T>> maybes)
		where T : class
		=> maybes.SelectMany(_ => _);

	/// <summary>
	/// Bind values via <see cref="functor"/>.
	/// <c>None</c>s are discarded but new <c>None</c>s can be produced
	/// by the functor.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IEnumerable<Maybe<TResult>> Bind<TSource, TResult>(
		this IEnumerable<Maybe<TSource>> maybes, Func<TSource, Maybe<TResult>> functor)
		where TSource : class
		where TResult : class
		=> maybes.SelectValues().Select(functor);

	/// <summary>
	/// Maps values via <see cref="functor"/>.
	/// <c>None</c>s are discarded.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IEnumerable<Maybe<TResult>> Map<TSource, TResult>(
		this IEnumerable<Maybe<TSource>> maybes, Func<TSource, TResult> functor)
		where TSource : class
		where TResult : class
		=> maybes.Bind(v => Prelude.Some(v).Map(functor));

	/// <summary>
	/// Filters <c>Some</c>s via <see cref="predicate"/>.
	/// <c>None</c>s are discarded.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IEnumerable<Maybe<TSource>> Where<TSource>(
		this IEnumerable<Maybe<TSource>> maybes, Func<TSource, bool> predicate)
		where TSource : class
		=> maybes.SelectValues().Where(predicate).Select(Prelude.Some);
	
	/// <summary>
	/// Filters <c>Some</c>s via negated <see cref="predicate"/>.
	/// <c>None</c>s are discarded.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IEnumerable<Maybe<TSource>> WhereNot<TSource>(
		this IEnumerable<Maybe<TSource>> maybes, Func<TSource, bool> predicate)
		where TSource : class
		=> maybes.SelectValues().Where(v => !predicate(v)).Select(Prelude.Some);
}