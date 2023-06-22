using System.Runtime.CompilerServices;
// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

namespace Bogoware.Monads;

public static class ResultBindAsyncExtensions
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TNewValue>> Bind<TValue, TNewValue>(
		this Task<Result<TValue>> result, Result<TNewValue> newValue)
		=> (await result).Bind(newValue);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TNewValue>> Bind<TValue, TNewValue>(
		this Task<Result<TValue>> result, Func<Result<TNewValue>> functor)
		=> (await result).Bind(functor);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TNewValue>>Bind<TValue, TNewValue>(
		this Task<Result<TValue>> result, Func<Task<Result<TNewValue>>> functor)
		=> await (await result).Bind(functor);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TNewValue>>Bind<TValue, TNewValue>(
		this Task<Result<TValue>> result, Func<TValue, Result<TNewValue>> functor)
		=> (await result).Bind(functor);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TNewValue>>Bind<TValue,TNewValue>(
		this Task<Result<TValue>> result, Func<TValue, Task<Result<TNewValue>>> functor)
		=> await (await result).Bind(functor);
}