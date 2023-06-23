using System.Runtime.CompilerServices;

// ReSharper disable MemberCanBePrivate.Global

namespace Bogoware.Monads;

public static class EnumerableResultExtensions
{
	/// <summary>
	/// Determines if all <see cref="Result{TValue}"/>s of a sequence are <c>Success</c>s.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool AllSuccess(this IEnumerable<IResult> successes)
		=> successes.All(_ => _.IsSuccess);

	/// <inheritdoc cref="AllSuccess"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool AllSuccess<TValue>(this IEnumerable<Result<TValue>> successes)
		=> successes.All(_ => _.IsSuccess);

	/// <summary>
	/// Determines if all <see cref="Result{TValue}"/>s of a sequence are <c>Failure</c>s.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool AllFailure(this IEnumerable<IResult> successes)
		=> successes.All(_ => _.IsFailure);

	/// <inheritdoc cref="AllFailure"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool AllFailure<TValue>(this IEnumerable<Result<TValue>> successes)
		=> successes.All(_ => _.IsFailure);

	/// <summary>
	/// Determines if any <see cref="Result{TValue}"/> of a sequence is <c>Success</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool AnySuccess(this IEnumerable<IResult> successes)
		=> successes.Any(_ => _.IsSuccess);

	/// <inheritdoc cref="AnySuccess"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool AnySuccess<TValue>(this IEnumerable<Result<TValue>> successes)
		=> successes.Any(_ => _.IsSuccess);

	/// <summary>
	/// Determines if any <see cref="Result{TValue}"/> of a sequence is <c>Failure</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool AnyFailure(this IEnumerable<IResult> successes)
		=> successes.Any(_ => _.IsFailure);

	/// <inheritdoc cref="AnyFailure"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool AnyFailure<TValue>(this IEnumerable<Result<TValue>> successes)
		=> successes.Any(_ => _.IsFailure);

	/// <summary>
	/// Extract values from <see cref="Result{TValue}"/>s.
	/// <c>Failure</c>s are discarded.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IEnumerable<TValue> SelectValues<TValue>(this IEnumerable<Result<TValue>> successes) 
		=> successes.SelectMany(_ => _);

	/// <summary>
	/// Bind values via the functor.
	/// <c>Failure</c>s are discarded but new <c>Failure</c>s can be produced
	/// by the functor.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IEnumerable<Result<TNewValue>> Bind<TValue, TNewValue>(
		this IEnumerable<Result<TValue>> successes, Func<TValue, Result<TNewValue>> functor)
		=> successes.SelectValues().Select(functor);

	/// <summary>
	/// Maps values via the functor.
	/// <c>Failure</c>s are discarded.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IEnumerable<Result<TNewValue>> Map<TValue, TNewValue>(
		this IEnumerable<Result<TValue>> successes, Func<TValue, TNewValue> functor)
		=> successes.Bind(v => new Result<TNewValue>(functor(v)));

	/// <summary>
	/// Matches results.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IEnumerable<TResult> Match<TValue, TResult>(
		this IEnumerable<Result<TValue>> results,
		Func<TValue, TResult> mapSuccesses,
		Func<Error, TResult> mapFailures)
		=> results.Select(result => result.Match(mapSuccesses, mapFailures));
	
	/// <inheritdoc cref="Match{TValue, TResult}(System.Collections.Generic.IEnumerable{Bogoware.Monads.Result{TValue}},System.Func{TValue,TResult},System.Func{Error,TResult})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IEnumerable<TResult> Match<TValue, TResult>(
		this IEnumerable<Result<TValue>> results,
		Func<TValue, TResult> mapSuccesses,
		TResult failure)
		=> results.Select(result => result.Match(mapSuccesses, failure));

	/// <summary>
	/// Filters <c>Success</c>es via the predicate.
	/// <c>Failure</c>s are discarded.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IEnumerable<Result<TValue>> Where<TValue>(
		this IEnumerable<Result<TValue>> successes, Func<TValue, bool> predicate)
		=> successes.SelectValues()
			.Where(predicate)
			.Select(v => new Result<TValue>(v));

	/// <summary>
	/// Filters <c>Success</c>es via negated predicate.
	/// <c>Failure</c>s are discarded.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IEnumerable<Result<TValue>> WhereNot<TValue>(
		this IEnumerable<Result<TValue>> successes, Func<TValue, bool> predicate)
		=> successes.SelectValues()
			.Where(v => !predicate(v))
			.Select(v => new Result<TValue>(v));
}