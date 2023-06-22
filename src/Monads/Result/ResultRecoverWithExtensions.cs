using System.Runtime.CompilerServices;
// ReSharper disable UnusedMember.Global

// ReSharper disable MemberCanBePrivate.Global

namespace Bogoware.Monads;

public static class ResultRecoverWithExtensions
{
	#region Functional Closure Extensions

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Result<TValue> RecoverWith<TValue, TError>(
		this Result<TValue> result, TValue newValue)
		where TError : Error
		=> result.IsSuccess ? result : new(newValue);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Result<TValue> RecoverWith<TValue, TError>(
		this Result<TValue> result, Func<TValue> functor)
		where TError : Error
		=> result.IsSuccess ? result : new(functor());

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TValue>> RecoverWith<TValue, TError>(
		this Result<TValue> result, Func<Task<TValue>> functor)
		where TError : Error
		=> result.IsSuccess ? result : new(await functor());

	#endregion Functional Closure Extensions

	#region Left Async Extensions

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TValue>> RecoverWith<TValue>(
		this Task<Result<TValue>> result, Func<Error, TValue> functor)
		=> (await result).RecoverWith(functor);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TValue>> RecoverWith<TValue>(
		this Task<Result<TValue>> result, Func<Error, Task<TValue>> functor)
		=> await (await result).RecoverWith(functor);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TValue>> RecoverWith<TValue>(
		this Task<Result<TValue>> result, TValue newValue)
		=> (await result).RecoverWith(newValue);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TValue>> RecoverWith<TValue>(
		this Task<Result<TValue>> result, Func<TValue> functor)
		=> (await result).RecoverWith(functor);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TValue>> RecoverWith<TValue>(
		this Task<Result<TValue>> result, Func<Task<TValue>> functor)
		=> await (await result).RecoverWith(functor);

	#endregion Left Async Extensions
}