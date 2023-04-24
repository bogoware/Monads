using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;

namespace Bogoware.Monads;

public static class ResultMapAsyncExtensions
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TNewValue, TError>> Map<TValue, TError, TNewValue>(
		this Task<Result<TValue, TError>> result, TNewValue newValue)
		where TError : Error
		=> (await result).Map(newValue);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TNewValue, TError>> Map<TValue, TError, TNewValue>(
		this Task<Result<TValue, TError>> result, Func<TNewValue> functor)
		where TError : Error
		=> (await result).Map(functor);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TNewValue, TError>> Map<TValue, TError, TNewValue>(
		this Task<Result<TValue, TError>> result, Func<Task<TNewValue>> functor)
		where TError : Error
		=> await (await result).Map(functor);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TNewValue, TError>> Map<TValue, TError, TNewValue>(
		this Task<Result<TValue, TError>> result, Func<TValue, TNewValue> functor)
		where TError : Error
		=> (await result).Map(functor);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TNewValue, TError>> Map<TValue, TError, TNewValue>(
		this Task<Result<TValue, TError>> result,
		Func<TValue, Task<TNewValue>> functor)
		where TError : Error
		=> await (await result).Map(functor);
}