using System.Runtime.CompilerServices;
// ReSharper disable UnusedMember.Global

namespace Bogoware.Monads;

public static class ResultEnsureExtensions
{
	#region Functional Closure Extensions

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Result<TValue> Ensure<TValue, TError>(
		this Result<TValue> result, Func<TValue, bool> predicate, Func<TError> error)
		where TError : Error
		=> result.Ensure(predicate, error());

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TValue>> Ensure<TValue, TError>(
		this Result<TValue> result, Func<TValue, bool> predicate, Func<Task<TError>> error)
		where TError : Error
		=> result.Ensure(predicate, await error());

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Task<Result<TValue>> Ensure<TValue, TError>(
		this Result<TValue> result, Func<TValue, Task<bool>> predicate, Func<TError> error)
		where TError : Error
		=> result.Ensure(predicate, error());

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TValue>> Ensure<TValue, TError>(
		this Result<TValue> result, Func<TValue, Task<bool>> predicate, Func<Task<TError>> error)
		where TError : Error
		=> await result.Ensure(predicate, await error());

	#endregion Functional Closure Extensions

	#region Left Async Extensions

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TValue>> Ensure<TValue>(
		this Task<Result<TValue>> result, Func<TValue, bool> predicate, Error error)
		=> (await result).Ensure(predicate, error);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TValue>> RecoverWith<TValue>(
		this Task<Result<TValue>> result, Func<TValue, Task<bool>> predicate, Error error)
		=> await (await result).Ensure(predicate, error);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TValue>> Ensure<TValue>(
		this Task<Result<TValue>> result, Func<TValue, bool> predicate, Func<Error> error)
		=> (await result).Ensure(predicate, error());

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TValue>> Ensure<TValue>(
		this Task<Result<TValue>> result, Func<TValue, bool> predicate, Func<Task<Error>> error)
		=> (await result).Ensure(predicate, await error());

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TValue>> Ensure<TValue>(
		this Task<Result<TValue>> result, Func<TValue, Task<bool>> predicate, Func<Error> error)
		=> await (await result).Ensure(predicate, error());

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TValue>> Ensure<TValue>(
		this Task<Result<TValue>> result, Func<TValue, Task<bool>> predicate, Func<Task<Error>> error)
		=> await (await result).Ensure(predicate, await error());

	#endregion Left Async Extensions
}