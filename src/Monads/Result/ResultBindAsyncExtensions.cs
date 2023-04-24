using System.Runtime.CompilerServices;

namespace Bogoware.Monads;

public static class ResultBindAsyncExtensions
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TNewValue, TError>> Bind<TValue, TError, TNewValue>(
		this Task<Result<TValue, TError>> result, Result<TNewValue, TError> newValue)
		where TError : Error
		=> (await result).Bind(newValue);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TNewValue, TError>> Bind<TValue, TError, TNewValue>(
		this Task<Result<TValue, TError>> result, Func<Result<TNewValue, TError>> functor)
		where TError : Error
		=> (await result).Bind(functor);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TNewValue, TError>>Bind<TValue, TError, TNewValue>(
		this Task<Result<TValue, TError>> result, Func<Task<Result<TNewValue, TError>>> functor)
		where TError : Error
		=> await (await result).Bind(functor);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TNewValue, TError>>Bind<TValue, TError, TNewValue>(
		this Task<Result<TValue, TError>> result, Func<TValue, Result<TNewValue, TError>> functor)
		where TError : Error
		=> (await result).Bind(functor);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TNewValue, TError>>Bind<TValue, TError, TNewValue>(
		this Task<Result<TValue, TError>> result, Func<TValue, Task<Result<TNewValue, TError>>> functor)
		where TError : Error
		=> await (await result).Bind(functor);
}