using System.Runtime.CompilerServices;

namespace Bogoware.Monads;

public static partial class ResultEnumerableExtensions
{

	/// <summary>
	/// Extract values from <see cref="Result{TValue}"/>s.
	/// <c>Failure</c>s are discarded.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IEnumerable<TValue> SelectValues<TValue>(this IEnumerable<Result<TValue>> successes)
		=> successes.SelectMany(v => v);
	
	/// <summary>
	/// Maps values via the functor.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IEnumerable<Result<TNewValue>> MapEach<TValue, TNewValue>(
		this IEnumerable<Result<TValue>> results, Func<TValue, TNewValue> functor)
		=> results.Select(result => result.Map(functor));

	/// <summary>
	/// Bind values via the functor.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IEnumerable<Result<TNewValue>> BindEach<TValue, TNewValue>(
		this IEnumerable<Result<TValue>> results, Func<TValue, Result<TNewValue>> functor)
		=> results.Select(result => result.Bind(functor));

	/// <summary>
	/// Matches results.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IEnumerable<TResult> MatchEach<TValue, TResult>(
		this IEnumerable<Result<TValue>> results,
		Func<TValue, TResult> mapSuccesses,
		Func<Error, TResult> mapFailures)
		=> results.Select(result => result.Match(mapSuccesses, mapFailures));

	/// <inheritdoc cref="MatchEach{TValue,TResult}(System.Collections.Generic.IEnumerable{Bogoware.Monads.Result{TValue}},System.Func{TValue,TResult},System.Func{Bogoware.Monads.Error,TResult})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IEnumerable<TResult> MatchEach<TValue, TResult>(
		this IEnumerable<Result<TValue>> results,
		Func<TValue, TResult> mapSuccesses,
		TResult failure)
		=> results.Select(result => result.Match(mapSuccesses, failure));
	
	/// <summary>
	/// Aggregates an enumeration of Result into a Result of an enumeration.
	/// If all <see cref="Result{TValue}"/>s are <c>Success</c> then return a <c>Success</c> <see cref="Result{TValue}"/>.
	/// otherwise return a <c>Failure</c> with an <see cref="AggregateError"/>.
	/// </summary>
	/// <param name="results"></param>
	/// <typeparam name="TValue"></typeparam>
	/// <returns></returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Result<IEnumerable<TValue?>> AggregateResults<TValue>(this IEnumerable<Result<TValue>> results)
		=> new ResultCollection<TValue>(results).ToResult();
}