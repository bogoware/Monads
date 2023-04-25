using System.Runtime.CompilerServices;
// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

namespace Bogoware.Monads;

public static class ResultMatchAsyncExtensions
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<TValue, TError, TResult>(
		this Task<Result<TValue, TError>> result, Func<TValue, TResult> successful, Func<TError, TResult> failure)
		where TError : Error
		=> (await result).Match(successful, failure);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<TValue, TError, TResult>(
		this Task<Result<TValue, TError>> result, Func<TValue, Task<TResult>> successful, Func<TError, TResult> failure)
		where TError : Error
		=> await (await result).Match(successful, failure);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<TValue, TError, TResult>(
		this Task<Result<TValue, TError>> result, Func<TValue, TResult> successful, Func<TError, Task<TResult>> failure)
		where TError : Error
		=> await (await result).Match(successful, failure);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<TValue, TError, TResult>(
		this Task<Result<TValue, TError>> result, Func<TValue, Task<TResult>> successful,
		Func<TError, Task<TResult>> failure)
		where TError : Error
		=> await (await result).Match(successful, failure);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<TValue, TError, TResult>(
		this Task<Result<TValue, TError>> result, TResult successful, TResult failure)
		where TError : Error
		=> (await result).Match(successful, failure);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<TValue, TError, TResult>(
		this Task<Result<TValue, TError>> result, Func<TResult> successful, TResult failure)
		where TError : Error
		=> (await result).Match(successful, failure);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<TValue, TError, TResult>(
		this Task<Result<TValue, TError>> result, Func<Task<TResult>> successful, TResult failure)
		where TError : Error
		=> await (await result).Match(successful, failure);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<TValue, TError, TResult>(
		this Task<Result<TValue, TError>> result, TResult successful, Func<TResult> failure)
		where TError : Error
		=> (await result).Match(successful, failure);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<TValue, TError, TResult>(
		this Task<Result<TValue, TError>> result, TResult successful, Func<Task<TResult>> failure)
		where TError : Error
		=> await (await result).Match(successful, failure);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<TValue, TError, TResult>(
		this Task<Result<TValue, TError>> result, Func<TResult> successful, Func<TResult> failure)
		where TError : Error
		=> (await result).Match(successful, failure);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<TValue, TError, TResult>(
		this Task<Result<TValue, TError>> result, Func<TValue, TResult> successful, TResult failure)
		where TError : Error
		=> (await result).Match(successful, failure);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<TValue, TError, TResult>(
		this Task<Result<TValue, TError>> result, Func<TValue, TResult> successful, Func<TResult> failure)
		where TError : Error
		=> (await result).Match(successful, failure);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<TValue, TError, TResult>(
		this Task<Result<TValue, TError>> result, TResult successful, Func<TError, Task<TResult>> failure)
		where TError : Error
		=> await (await result).Match(successful, failure);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<TValue, TError, TResult>(
		this Task<Result<TValue, TError>> result, Func<TResult> successful, Func<TError, Task<TResult>> failure)
		where TError : Error
		=> await (await result).Match(successful, failure);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<TValue, TError, TResult>(
		this Task<Result<TValue, TError>> result, Func<TValue, Task<TResult>> successful, TResult failure)
		where TError : Error
		=> await (await result).Match(successful, failure);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<TValue, TError, TResult>(
		this Task<Result<TValue, TError>> result, Func<TValue, Task<TResult>> successful, Func<TResult> failure)
		where TError : Error
		=> await (await result).Match(successful, failure);
}