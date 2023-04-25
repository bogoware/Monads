using System.Runtime.CompilerServices;

// ReSharper disable MemberCanBePrivate.Global

namespace Bogoware.Monads;

public static class ResultExecuteExtensions
{
	#region Functional Closure Extensions

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Result<TValue, TError> ExecuteIfSuccess<TValue, TError>(
		this Result<TValue, TError> result, Action action)
		where TError : Error
	{
		if (result.IsSuccess) action();
		return result;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TValue, TError>> ExecuteIfSuccess<TValue, TError>(
		this Result<TValue, TError> result, Func<Task> action)
		where TError : Error
	{
		if (result.IsSuccess) await action();
		return result;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Result<TValue, TError> ExecuteIfFailure<TValue, TError>(
		this Result<TValue, TError> result, Action action)
		where TError : Error
	{
		if (result.IsFailure) action();
		return result;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TValue, TError>> ExecuteIfFailure<TValue, TError>(
		this Result<TValue, TError> result, Func<Task> action)
		where TError : Error
	{
		if (result.IsFailure) await action();
		return result;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Result<TValue, TError> Execute<TValue, TError>(this Result<TValue, TError> result, Action action)
		where TError : Error
	{
		action();
		return result;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TValue, TError>> Execute<TValue, TError>(
		this Result<TValue, TError> result, Func<Task> action)
		where TError : Error
	{
		await action();
		return result;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Result<TValue, TError> Execute<TValue, TError>(
		this Result<TValue, TError> result, Action<Result<TValue, TError>> action)
		where TError : Error
	{
		action(result);
		return result;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TValue, TError>> Execute<TValue, TError>(
		this Result<TValue, TError> result, Func<Result<TValue, TError>, Task> action)
		where TError : Error
	{
		await action(result);
		return result;
	}

	#endregion Functional Closure Extensions

	#region Left Async Extensions

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TValue, TError>> ExecuteIfSuccess<TValue, TError>(
		this Task<Result<TValue, TError>> result, Action<TValue> action)
		where TError : Error
		=> (await result).ExecuteIfSuccess(action);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TValue, TError>> ExecuteIfSuccess<TValue, TError>(
		this Task<Result<TValue, TError>> result, Func<TValue, Task> action)
		where TError : Error
		=> await (await result).ExecuteIfSuccess(action);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TValue, TError>> ExecuteIfFailure<TValue, TError>(
		this Task<Result<TValue, TError>> result, Action<TError> action)
		where TError : Error
		=> (await result).ExecuteIfFailure(action);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TValue, TError>> ExecuteIfFailure<TValue, TError>(
		this Task<Result<TValue, TError>> result, Func<TError, Task> action)
		where TError : Error
		=> await (await result).ExecuteIfFailure(action);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TValue, TError>> ExecuteIfSuccess<TValue, TError>(
		this Task<Result<TValue, TError>> result, Action action)
		where TError : Error
		=> (await result).ExecuteIfSuccess(action);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TValue, TError>> ExecuteIfSuccess<TValue, TError>(
		this Task<Result<TValue, TError>> result, Func<Task> action)
		where TError : Error
		=> await (await result).ExecuteIfSuccess(action);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TValue, TError>> ExecuteIfFailure<TValue, TError>(
		this Task<Result<TValue, TError>> result, Action action)
		where TError : Error
		=> (await result).ExecuteIfFailure(action);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TValue, TError>> ExecuteIfFailure<TValue, TError>(
		this Task<Result<TValue, TError>> result, Func<Task> action)
		where TError : Error
		=> await (await result).ExecuteIfFailure(action);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TValue, TError>> Execute<TValue, TError>(
		this Task<Result<TValue, TError>> result, Action action)
		where TError : Error
		=> (await result).Execute(action);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TValue, TError>> Execute<TValue, TError>(
		this Task<Result<TValue, TError>> result, Func<Task> action)
		where TError : Error
		=> await (await result).Execute(action);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TValue, TError>> Execute<TValue, TError>(
		this Task<Result<TValue, TError>> resultTask, Action<Result<TValue, TError>> action)
		where TError : Error
	{
		var result = await resultTask;
		action(result);
		return result;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TValue, TError>> Execute<TValue, TError>(
		this Task<Result<TValue, TError>> resultTask, Func<Result<TValue, TError>, Task> action)
		where TError : Error
	{
		var result = await resultTask;
		await action(result);
		return result;
	}

	#endregion Left Async Extensions
}