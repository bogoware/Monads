using System.Runtime.CompilerServices;
// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

namespace Bogoware.Monads;

public static class ResultMapAsyncExtensions
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TNewValue>> Map<TValue, TError, TNewValue>(
		this Task<Result<TValue>> result, TNewValue newValue)
		where TError : Error
		=> (await result).Map(newValue);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TNewValue>> Map<TValue, TError, TNewValue>(
		this Task<Result<TValue>> result, Func<TNewValue> functor)
		where TError : Error
		=> (await result).Map(functor);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TNewValue>> Map<TValue, TError, TNewValue>(
		this Task<Result<TValue>> result, Func<Task<TNewValue>> functor)
		where TError : Error
		=> await (await result).Map(functor);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TNewValue>> Map<TValue, TError, TNewValue>(
		this Task<Result<TValue>> result, Func<TValue, TNewValue> functor)
		where TError : Error
		=> (await result).Map(functor);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TNewValue>> Map<TValue, TError, TNewValue>(
		this Task<Result<TValue>> result,
		Func<TValue, Task<TNewValue>> functor)
		where TError : Error
		=> await (await result).Map(functor);
}