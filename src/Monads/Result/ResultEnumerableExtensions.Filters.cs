using System.Runtime.CompilerServices;

namespace Bogoware.Monads;

public static partial class ResultEnumerableExtensions
{

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