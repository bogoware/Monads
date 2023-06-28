using System.Runtime.CompilerServices;
// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

namespace Bogoware.Monads;

public static class ResultMapAsyncExtensions
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TNewValue>> Map<TValue, TNewValue>(
		this Task<Result<TValue>> result, TNewValue newValue)
		=> (await result).Map(newValue);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TNewValue>> Map<TValue, TNewValue>(
		this Task<Result<TValue>> result, Func<TNewValue> functor)
		=> (await result).Map(functor);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TNewValue>> Map<TValue, TNewValue>(
		this Task<Result<TValue>> result, Func<Task<TNewValue>> functor)
		=> await (await result).Map(functor);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TNewValue>> Map<TValue, TNewValue>(
		this Task<Result<TValue>> result, Func<TValue, TNewValue> functor)
		=> (await result).Map(functor);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TNewValue>> Map<TValue, TNewValue>(
		this Task<Result<TValue>> result,
		Func<TValue, Task<TNewValue>> functor)
		=> await (await result).Map(functor);
	
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<Unit>> Map<TValue>(
		this Task<Result<TValue>> resultTask,
		Action<TValue> functor)
	{
		var result = await resultTask;
		if (result.IsFailure) return Result.Failure<Unit>(result.GetErrorOrThrow());
		functor(result.GetValueOrThrow());
		return Result.Unit;
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<Unit>> Map<TValue>(
		this Task<Result<TValue>> resultTask,
		Func<TValue, Task> functor)
	{
		var result = await resultTask;
		if (result.IsFailure) return Result.Failure<Unit>(result.GetErrorOrThrow());
		await functor(result.GetValueOrThrow());
		return Result.Unit;
	}
}