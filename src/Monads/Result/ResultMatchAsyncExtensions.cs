using System.Runtime.CompilerServices;
// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

namespace Bogoware.Monads;

public static class ResultMatchAsyncExtensions
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<TValue, TResult>(
		this Task<Result<TValue>> result, Func<TValue, TResult> successful, Func<Error, TResult> failure)
		=> (await result).Match(successful, failure);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<TValue, TResult>(
		this Task<Result<TValue>> result, Func<TValue, Task<TResult>> successful, Func<Error, TResult> failure)
		=> await (await result).Match(successful, failure);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<TValue, TResult>(
		this Task<Result<TValue>> result, Func<TValue, TResult> successful, Func<Error, Task<TResult>> failure)
		=> await (await result).Match(successful, failure);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<TValue, TResult>(
		this Task<Result<TValue>> result, Func<TValue, Task<TResult>> successful,
		Func<Error, Task<TResult>> failure)
		=> await (await result).Match(successful, failure);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<TValue, TResult>(
		this Task<Result<TValue>> result, TResult successful, TResult failure)
		=> (await result).Match(successful, failure);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<TValue, TResult>(
		this Task<Result<TValue>> result, Func<TResult> successful, TResult failure)
		=> (await result).Match(successful, failure);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<TValue, TResult>(
		this Task<Result<TValue>> result, Func<Task<TResult>> successful, TResult failure)
		=> await (await result).Match(successful, failure);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<TValue, TResult>(
		this Task<Result<TValue>> result, TResult successful, Func<TResult> failure)
		=> (await result).Match(successful, failure);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<TValue, TResult>(
		this Task<Result<TValue>> result, TResult successful, Func<Task<TResult>> failure)
		=> await (await result).Match(successful, failure);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<TValue, TResult>(
		this Task<Result<TValue>> result, Func<TResult> successful, Func<TResult> failure)
		=> (await result).Match(successful, failure);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<TValue, TResult>(
		this Task<Result<TValue>> result, Func<TValue, TResult> successful, TResult failure)
		=> (await result).Match(successful, failure);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<TValue, TResult>(
		this Task<Result<TValue>> result, Func<TValue, TResult> successful, Func<TResult> failure)
		=> (await result).Match(successful, failure);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<TValue, TResult>(
		this Task<Result<TValue>> result, TResult successful, Func<Error, Task<TResult>> failure)
		=> await (await result).Match(successful, failure);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<TValue, TResult>(
		this Task<Result<TValue>> result, Func<TResult> successful, Func<Error, Task<TResult>> failure)
		=> await (await result).Match(successful, failure);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<TValue, TResult>(
		this Task<Result<TValue>> result, Func<TValue, Task<TResult>> successful, TResult failure)
		=> await (await result).Match(successful, failure);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<TValue, TResult>(
		this Task<Result<TValue>> result, Func<TValue, Task<TResult>> successful, Func<TResult> failure)
		=> await (await result).Match(successful, failure);
}