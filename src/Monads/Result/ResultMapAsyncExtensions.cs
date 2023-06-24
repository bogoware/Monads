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
}