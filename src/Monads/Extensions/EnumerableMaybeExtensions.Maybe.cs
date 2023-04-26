using System.Runtime.CompilerServices;

// ReSharper disable MemberCanBePrivate.Global

namespace Bogoware.Monads;

public static class EnumerableMaybeExtensions
{
	/// <summary>
	/// Determines if all <see cref="Maybe{T}"/>s of a sequence are <c>Some</c>s.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool AllSome(this IEnumerable<IMaybe> maybes)
		=> maybes.All(_ => _.IsSome);

	/// <inheritdoc cref="AllSome"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool AllSome<TValue>(this IEnumerable<Maybe<TValue>> maybes) where TValue : class
		=> maybes.All(_ => _.IsSome);

	/// <summary>
	/// Determines if all <see cref="Maybe{T}"/>s of a sequence are <c>None</c>s.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool AllNone(this IEnumerable<IMaybe> maybes)
		=> maybes.All(_ => _.IsNone);

	/// <inheritdoc cref="AllNone"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool AllNone<TValue>(this IEnumerable<Maybe<TValue>> maybes) where TValue : class
		=> maybes.All(_ => _.IsNone);

	/// <summary>
	/// Determines if any <see cref="Maybe{T}"/> of a sequence is <c>Some</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool AnySome(this IEnumerable<IMaybe> maybes)
		=> maybes.Any(_ => _.IsSome);

	/// <inheritdoc cref="AnySome"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool AnySome<TValue>(this IEnumerable<Maybe<TValue>> maybes) where TValue : class
		=> maybes.Any(_ => _.IsSome);

	/// <summary>
	/// Determines if any <see cref="Maybe{T}"/> of a sequence is <c>None</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool AnyNone(this IEnumerable<IMaybe> maybes)
		=> maybes.Any(_ => _.IsNone);

	/// <inheritdoc cref="AnyNone"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool AnyNone<TValue>(this IEnumerable<Maybe<TValue>> maybes) where TValue : class => maybes.Any(_ => _.IsNone);

	/// <summary>
	/// Extract values from <see cref="Maybe{T}"/>s.
	/// <c>None</c>s are discarded.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IEnumerable<TValue> SelectValues<TValue>(this IEnumerable<Maybe<TValue>> maybes)
		where TValue : class
		=> maybes.SelectMany(_ => _);

	/// <summary>
	/// Bind values via the functor.
	/// <c>None</c>s are discarded but new <c>None</c>s can be produced
	/// by the functor.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IEnumerable<Maybe<TNewValue>> Bind<TValue, TNewValue>(
		this IEnumerable<Maybe<TValue>> maybes, Func<TValue, Maybe<TNewValue>> functor)
		where TValue : class
		where TNewValue : class
		=> maybes.SelectValues().Select(functor);

	/// <summary>
	/// Maps values via the functor.
	/// <c>None</c>s are discarded.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IEnumerable<Maybe<TNewValue>> Map<TValue, TNewValue>(
		this IEnumerable<Maybe<TValue>> maybes, Func<TValue, TNewValue> functor)
		where TValue : class
		where TNewValue : class
		=> maybes.Bind(v => Prelude.Some(v).Map(functor));

	/// <summary>
	/// Filters <c>Some</c>s via the predicate.
	/// <c>None</c>s are discarded.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IEnumerable<Maybe<TValue>> Where<TValue>(
		this IEnumerable<Maybe<TValue>> maybes, Func<TValue, bool> predicate)
		where TValue : class
		=> maybes.SelectValues().Where(predicate).Select(Prelude.Some);

	/// <summary>
	/// Filters <c>Some</c>s via negated predicate.
	/// <c>None</c>s are discarded.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IEnumerable<Maybe<TValue>> WhereNot<TValue>(
		this IEnumerable<Maybe<TValue>> maybes, Func<TValue, bool> predicate)
		where TValue : class
		=> maybes.SelectValues().Where(v => !predicate(v)).Select(Prelude.Some);
}