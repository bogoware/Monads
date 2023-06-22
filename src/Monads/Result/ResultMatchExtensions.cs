using System.Runtime.CompilerServices;
// ReSharper disable MemberCanBePrivate.Global

namespace Bogoware.Monads;

public static class ResultMatchExtensions
{
	#region Match Functional Closure
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TResult Match<TValue, TResult>(
		this Result<TValue> result, TResult successful, TResult failure)
		=> result.IsSuccess ? successful : failure;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TResult Match<TValue, TResult>(
		this Result<TValue> result, Func<TResult> successful, TResult failure)
		=> result.IsSuccess ? successful() : failure;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Task<TResult> Match<TValue,TResult>(
		this Result<TValue> result, Func<Task<TResult>> successful, TResult failure)
		=> result.IsSuccess ? successful() : Task.FromResult(failure);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TResult Match<TValue, TResult>(
		this Result<TValue> result, TResult successful, Func<TResult> failure)
		=> result.IsSuccess ? successful : failure();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Task<TResult> Match<TValue, TResult>(
		this Result<TValue> result, TResult successful, Func<Task<TResult>> failure)
		=> result.IsSuccess ? Task.FromResult(successful) : failure();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TResult Match<TValue, TResult>(
		this Result<TValue> result, Func<TResult> successful, Func<TResult> failure)
		=> result.IsSuccess ? successful() : failure();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TResult Match<TValue, TResult>(
		this Result<TValue> result, Func<TValue, TResult> successful, TResult failure)
		=> result.Match(successful, _ => failure);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TResult Match<TValue, TResult>(
		this Result<TValue> result, Func<TValue, TResult> successful, Func<TResult> failure)
		=> result.Match(successful, _ => failure());
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<TValue, TResult>(
		this Result<TValue> result, Func<TValue, TResult> successful, Func<Task<TResult>> failure)
		=> await result.Match(successful, _ => failure());

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Task<TResult> Match<TValue,TResult>(
		this Result<TValue> result, TResult successful, Func<Error, Task<TResult>> failure)
		=> result.Match(_ => successful, failure);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Task<TResult> Match<TValue, TResult>(
		this Result<TValue> result, Func<TResult> successful, Func<Error, Task<TResult>> failure)
		=> result.Match(_ => successful(), failure);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Task<TResult> Match<TValue, TResult>(
		this Result<TValue> result, Func<TValue, Task<TResult>> successful, TResult failure)
		=> result.Match(successful, _ => failure);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Task<TResult> Match<TValue, TResult>(
		this Result<TValue> result, Func<TValue, Task<TResult>> successful, Func<TResult> failure)
		=> result.Match(successful, _ => failure());
	#endregion Match Functional Closure

	#region Utils based on Match
	/// <summary>
	/// Retrieve the value if <see cref="Result{TValue}.IsSuccess"/> or return the <c>recoverValue</c> if <see cref="Result{TValue}.IsFailure"/>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TValue GetValue<TValue>(this Result<TValue> result, TValue recoverValue)
		=> result.Match(successful => successful, recoverValue);

	/// <inheritdoc cref="GetValue{TValue}(Bogoware.Monads.Result{TValue},TValue)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TValue GetValue<TValue>(
		this Result<TValue> result, Func<TValue> recoverValue)
		=> result.Match(successful => successful, recoverValue);
	
	/// <inheritdoc cref="GetValue{TValue}(Bogoware.Monads.Result{TValue},TValue)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Task<TValue> GetValue<TValue>(
		this Result<TValue> result, Func<Task<TValue>> recoverValue)
		=> result.Match(successful => successful, recoverValue);
	
	#endregion Utils based on Match
}