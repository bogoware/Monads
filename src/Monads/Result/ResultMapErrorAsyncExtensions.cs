using System.Runtime.CompilerServices;

namespace Bogoware.Monads;

public static class ResultMapErrorAsyncExtensions
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TValue>> MapError<TValue>(
		this Task<Result<TValue>> result, Error newError)
		=> (await result).MapError(newError);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TValue>> Map<TValue>(
		this Task<Result<TValue>> result, Func<Error> functor)
		=> (await result).MapError(functor);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TValue>> Map<TValue>(
		this Task<Result<TValue>> result, Func<Task<Error>> functor)
		=> await (await result).MapError(functor);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TValue>> Map<TValue>(
		this Task<Result<TValue>> result, Func<Error, Error> functor)
		=> (await result).MapError(functor);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TValue>> Map<TValue>(
		this Task<Result<TValue>> result,
		Func<Error, Task<Error>> functor)
		=> await (await result).MapError(functor);
}