using System.Runtime.CompilerServices;

// ReSharper disable MemberCanBePrivate.Global

namespace Bogoware.Monads;

public static class EnumerableResultExtensions
{
	/// <summary>
	/// Determines if all <see cref="Result{TValue,TError}"/>s of a sequence are <c>Success</c>s.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool AllSuccess(this IEnumerable<IResult> successes)
		=> successes.All(_ => _.IsSuccess);

	/// <inheritdoc cref="AllSuccess"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool AllSuccess<TValue, TError>(this IEnumerable<Result<TValue, TError>> successes)
		where TError : Error
		=> successes.All(_ => _.IsSuccess);

	/// <summary>
	/// Determines if all <see cref="Result{TValue,TError}"/>s of a sequence are <c>Failure</c>s.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool AllFailure(this IEnumerable<IResult> successes)
		=> successes.All(_ => _.IsFailure);

	/// <inheritdoc cref="AllFailure"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool AllFailure<TValue, TError>(this IEnumerable<Result<TValue, TError>> successes)
		where TError : Error
		=> successes.All(_ => _.IsFailure);

	/// <summary>
	/// Determines if any <see cref="Result{TValue,TError}"/> of a sequence is <c>Success</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool AnySuccess(this IEnumerable<IResult> successes)
		=> successes.Any(_ => _.IsSuccess);

	/// <inheritdoc cref="AnySuccess"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool AnySuccess<TValue, TError>(this IEnumerable<Result<TValue, TError>> successes)
		where TError : Error
		=> successes.Any(_ => _.IsSuccess);

	/// <summary>
	/// Determines if any <see cref="Result{TValue,TError}"/> of a sequence is <c>Failure</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool AnyFailure(this IEnumerable<IResult> successes)
		=> successes.Any(_ => _.IsFailure);

	/// <inheritdoc cref="AnyFailure"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool AnyFailure<TValue, TError>(this IEnumerable<Result<TValue, TError>> successes)
		where TError : Error
		=> successes.Any(_ => _.IsFailure);

	/// <summary>
	/// Extract values from <see cref="Result{TValue,TError}"/>s.
	/// <c>Failure</c>s are discarded.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IEnumerable<TValue> SelectValues<TValue, TError>(this IEnumerable<Result<TValue, TError>> successes)
		where TError : Error
		=> successes.SelectMany(_ => _);

	/// <summary>
	/// Bind values via the functor.
	/// <c>Failure</c>s are discarded but new <c>Failure</c>s can be produced
	/// by the functor.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IEnumerable<Result<TNewValue, TError>> Bind<TValue, TError, TNewValue>(
		this IEnumerable<Result<TValue, TError>> successes, Func<TValue, Result<TNewValue, TError>> functor)
		where TError : Error
		=> successes.SelectValues().Select(functor);

	/// <summary>
	/// Maps values via the functor.
	/// <c>Failure</c>s are discarded.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IEnumerable<Result<TNewValue, TError>> Map<TValue, TError, TNewValue>(
		this IEnumerable<Result<TValue, TError>> successes, Func<TValue, TNewValue> functor)
		where TError : Error
		=> successes.Bind(v => Prelude.Success<TNewValue, TError>(functor(v)));

	/// <summary>
	/// Matches results.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IEnumerable<TResult> Match<TValue, TError, TResult>(
		this IEnumerable<Result<TValue, TError>> results,
		Func<TValue, TResult> mapSuccesses,
		Func<TError, TResult> mapFailures)
		where TError : Error
		=> results.Select(result => result.Match(mapSuccesses, mapFailures));
	
	/// <inheritdoc cref="Match{TValue,TError,TResult}(System.Collections.Generic.IEnumerable{Bogoware.Monads.Result{TValue,TError}},System.Func{TValue,TResult},System.Func{TError,TResult})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IEnumerable<TResult> Match<TValue, TError, TResult>(
		this IEnumerable<Result<TValue, TError>> results,
		Func<TValue, TResult> mapSuccesses,
		TResult failure)
		where TError : Error
		=> results.Select(result => result.Match(mapSuccesses, failure));

	/// <summary>
	/// Filters <c>Success</c>es via the predicate.
	/// <c>Failure</c>s are discarded.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IEnumerable<Result<TValue, TError>> Where<TValue, TError>(
		this IEnumerable<Result<TValue, TError>> successes, Func<TValue, bool> predicate)
		where TError : Error
		=> successes.SelectValues()
			.Where(predicate)
			.Select(Prelude.Success<TValue, TError>);

	/// <summary>
	/// Filters <c>Success</c>es via negated predicate.
	/// <c>Failure</c>s are discarded.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IEnumerable<Result<TValue, TError>> WhereNot<TValue, TError>(
		this IEnumerable<Result<TValue, TError>> successes, Func<TValue, bool> predicate)
		where TError : Error
		=> successes.SelectValues()
			.Where(v => !predicate(v))
			.Select(Prelude.Success<TValue, TError>);
}