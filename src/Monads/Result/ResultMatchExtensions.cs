using System.Runtime.CompilerServices;

namespace Bogoware.Monads;

public static class ResultMatchExtensions
{
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
}