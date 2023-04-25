using System.Runtime.CompilerServices;
// ReSharper disable MemberCanBePrivate.Global

namespace Bogoware.Monads;

public static class ResultMatchExtensions
{
	#region Match Functional Closure
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TResult Match<TValue, TError, TResult>(
		this Result<TValue, TError> result, TResult successful, TResult failure)
		where TError : Error
		=> result.IsSuccess ? successful : failure;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TResult Match<TValue, TError, TResult>(
		this Result<TValue, TError> result, Func<TResult> successful, TResult failure)
		where TError : Error
		=> result.IsSuccess ? successful() : failure;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Task<TResult> Match<TValue, TError, TResult>(
		this Result<TValue, TError> result, Func<Task<TResult>> successful, TResult failure)
		where TError : Error
		=> result.IsSuccess ? successful() : Task.FromResult(failure);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TResult Match<TValue, TError, TResult>(
		this Result<TValue, TError> result, TResult successful, Func<TResult> failure)
		where TError : Error
		=> result.IsSuccess ? successful : failure();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Task<TResult> Match<TValue, TError, TResult>(
		this Result<TValue, TError> result, TResult successful, Func<Task<TResult>> failure)
		where TError : Error
		=> result.IsSuccess ? Task.FromResult(successful) : failure();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TResult Match<TValue, TError, TResult>(
		this Result<TValue, TError> result, Func<TResult> successful, Func<TResult> failure)
		where TError : Error
		=> result.IsSuccess ? successful() : failure();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TResult Match<TValue, TError, TResult>(
		this Result<TValue, TError> result, Func<TValue, TResult> successful, TResult failure)
		where TError : Error
		=> result.Match(successful, _ => failure);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TResult Match<TValue, TError, TResult>(
		this Result<TValue, TError> result, Func<TValue, TResult> successful, Func<TResult> failure)
		where TError : Error
		=> result.Match(successful, _ => failure());
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<TValue, TError, TResult>(
		this Result<TValue, TError> result, Func<TValue, TResult> successful, Func<Task<TResult>> failure)
		where TError : Error
		=> await result.Match(successful, _ => failure());

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Task<TResult> Match<TValue, TError, TResult>(
		this Result<TValue, TError> result, TResult successful, Func<TError, Task<TResult>> failure)
		where TError : Error
		=> result.Match(_ => successful, failure);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Task<TResult> Match<TValue, TError, TResult>(
		this Result<TValue, TError> result, Func<TResult> successful, Func<TError, Task<TResult>> failure)
		where TError : Error
		=> result.Match(_ => successful(), failure);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Task<TResult> Match<TValue, TError, TResult>(
		this Result<TValue, TError> result, Func<TValue, Task<TResult>> successful, TResult failure)
		where TError : Error
		=> result.Match(successful, _ => failure);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Task<TResult> Match<TValue, TError, TResult>(
		this Result<TValue, TError> result, Func<TValue, Task<TResult>> successful, Func<TResult> failure)
		where TError : Error
		=> result.Match(successful, _ => failure());
	#endregion Match Functional Closure

	#region Utils based on Match
	/// <summary>
	/// Retrieve the value if <see cref="Result{TValue,TError}.IsSuccess"/> or return the <c>recoverValue</c> if <see cref="Result{TValue,TError}.IsFailure"/>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TValue GetValue<TValue, TError>(this Result<TValue, TError> result, TValue recoverValue)
		where TError : Error
		=> result.Match(successful => successful, recoverValue);

	/// <inheritdoc cref="GetValue{TValue,TError}(Bogoware.Monads.Result{TValue,TError},TValue)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TValue GetValue<TValue, TError>(
		this Result<TValue, TError> result, Func<TValue> recoverValue)
		where TError : Error
		=> result.Match(successful => successful, recoverValue);
	
	/// <inheritdoc cref="GetValue{TValue,TError}(Bogoware.Monads.Result{TValue,TError},TValue)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Task<TValue> GetValue<TValue, TError>(
		this Result<TValue, TError> result, Func<Task<TValue>> recoverValue)
		where TError : Error
		=> result.Match(successful => successful, recoverValue);
	
	#endregion Utils based on Match
}