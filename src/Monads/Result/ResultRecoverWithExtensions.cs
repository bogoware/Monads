using System.Globalization;
using System.Runtime.CompilerServices;

// ReSharper disable MemberCanBePrivate.Global

namespace Bogoware.Monads;

public static class ResultRecoverWithExtensions
{
	#region Functional Closure Extensions

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Result<TValue, TError> RecoverWith<TValue, TError>(
		this Result<TValue, TError> result, TValue newValue)
		where TError : Error
		=> result.IsSuccess ? result : new(newValue);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Result<TValue, TError> RecoverWith<TValue, TError>(
		this Result<TValue, TError> result, Func<TValue> functor)
		where TError : Error
		=> result.IsSuccess ? result : new(functor());

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TValue, TError>> RecoverWith<TValue, TError>(
		this Result<TValue, TError> result, Func<Task<TValue>> functor)
		where TError : Error
		=> result.IsSuccess ? result : new(await functor());

	#endregion Functional Closure Extensions

	#region Left Async Extensions

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TValue, TError>> RecoverWith<TValue, TError>(
		this Task<Result<TValue, TError>> result, Func<TError, TValue> functor)
		where TError : Error
		=> (await result).RecoverWith(functor);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TValue, TError>> RecoverWith<TValue, TError>(
		this Task<Result<TValue, TError>> result, Func<TError, Task<TValue>> functor)
		where TError : Error
		=> await (await result).RecoverWith(functor);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TValue, TError>> RecoverWith<TValue, TError>(
		this Task<Result<TValue, TError>> result, TValue newValue)
		where TError : Error
		=> (await result).RecoverWith(newValue);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TValue, TError>> RecoverWith<TValue, TError>(
		this Task<Result<TValue, TError>> result, Func<TValue> functor)
		where TError : Error
		=> (await result).RecoverWith(functor);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<TValue, TError>> RecoverWith<TValue, TError>(
		this Task<Result<TValue, TError>> result, Func<Task<TValue>> functor)
		where TError : Error
		=> await (await result).RecoverWith(functor);

	#endregion Left Async Extensions
}