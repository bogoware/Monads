using System.Runtime.CompilerServices;

namespace Bogoware.Monads;

public static class ResultSatisfyExtensions
{
	#region Functional Closure

	/// <summary>
	/// Evaluate the <c>predicate</c> applied to the value if present.
	/// Return <c>false</c> in case of <c>None</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool Satisfy<TValue, TError>(this Result<TValue, TError> maybe, Func<TValue, bool> predicate)
		where TValue : class where TError : Error
		=> maybe.Match(predicate, false);

	/// <inheritdoc cref="Satisfy{TValue,TError}(Bogoware.Monads.Result{TValue,TError},System.Func{TValue,bool})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Task<bool> Satisfy<TValue, TError>(
		this Result<TValue, TError> maybe, Func<TValue, Task<bool>> predicate)
		where TValue : class where TError : Error
		=> maybe.Match(predicate, false);

	#endregion Functional Closure
	
	#region Left Async Closure
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Task<bool> Satisfy<TValue, TError>(this Task<Result<TValue, TError>> maybe, Func<TValue, bool> predicate)
		where TValue : class where TError : Error
		=> maybe.Match(predicate, false);

	/// <inheritdoc cref="Satisfy{TValue,TError}(Bogoware.Monads.Result{TValue,TError},System.Func{TValue,bool})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Task<bool> Satisfy<TValue, TError>(
		this Task<Result<TValue, TError>> maybe, Func<TValue, Task<bool>> predicate)
		where TValue : class where TError : Error
		=> maybe.Match(predicate, false);

	#endregion Left Async Closure
}