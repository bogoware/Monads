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
	public static bool Satisfy<TValue>(this Result<TValue> result, Func<TValue, bool> predicate)
		where TValue : class
		=> result.Match(predicate, false);

	/// <inheritdoc cref="Satisfy{TValue}(Bogoware.Monads.Result{TValue},System.Func{TValue,bool})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Task<bool> Satisfy<TValue>(
		this Result<TValue> result, Func<TValue, Task<bool>> predicate)
		where TValue : class
		=> result.Match(predicate, false);

	#endregion Functional Closure
	
	#region Left Async Closure
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Task<bool> Satisfy<TValue>(this Task<Result<TValue>> result, Func<TValue, bool> predicate)
		where TValue : class
		=> result.Match(predicate, false);

	/// <inheritdoc cref="Satisfy{TValue}(Bogoware.Monads.Result{TValue},System.Func{TValue,bool})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Task<bool> Satisfy<TValue>(
		this Task<Result<TValue>> maybe, Func<TValue, Task<bool>> predicate)
		where TValue : class
		=> maybe.Match(predicate, false);

	#endregion Left Async Closure
}