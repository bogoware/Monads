using System.Runtime.CompilerServices;

// ReSharper disable MemberCanBePrivate.Global

namespace Bogoware.Monads;

public static partial class ResultEnumerableExtensions
{
	/// <summary>
	/// Determines if all <see cref="Result{TValue}"/>s of a sequence are <c>Success</c>s.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool AllSuccess(this IEnumerable<IResult> successes)
		=> successes.All(r => r.IsSuccess);

	/// <inheritdoc cref="AllSuccess"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool AllSuccess<TValue>(this IEnumerable<Result<TValue>> successes)
		=> successes.All(v => v.IsSuccess);

	/// <summary>
	/// Determines if all <see cref="Result{TValue}"/>s of a sequence are <c>Failure</c>s.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool AllFailure(this IEnumerable<IResult> successes)
		=> successes.All(r => r.IsFailure);

	/// <inheritdoc cref="AllFailure"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool AllFailure<TValue>(this IEnumerable<Result<TValue>> successes)
		=> successes.All(v => v.IsFailure);

	/// <summary>
	/// Determines if any <see cref="Result{TValue}"/> of a sequence is <c>Success</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool AnySuccess(this IEnumerable<IResult> successes)
		=> successes.Any(r => r.IsSuccess);

	/// <inheritdoc cref="AnySuccess"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool AnySuccess<TValue>(this IEnumerable<Result<TValue>> successes)
		=> successes.Any(v => v.IsSuccess);

	/// <summary>
	/// Determines if any <see cref="Result{TValue}"/> of a sequence is <c>Failure</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool AnyFailure(this IEnumerable<IResult> successes)
		=> successes.Any(r => r.IsFailure);

	/// <inheritdoc cref="AnyFailure"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool AnyFailure<TValue>(this IEnumerable<Result<TValue>> successes)
		=> successes.Any(v => v.IsFailure);

}