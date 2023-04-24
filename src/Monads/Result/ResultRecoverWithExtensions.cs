using System.Globalization;
// ReSharper disable MemberCanBePrivate.Global

namespace Bogoware.Monads;

public static class ResultRecoverWithExtensions
{
	#region Functional Closure Extensions
	public static Result<TValue, TError> RecoverWith<TValue, TError>(
		this Result<TValue, TError> result, TValue newValue)
		where TError : Error
		=> result.IsSuccess ? result : new(newValue);

	public static Result<TValue, TError> RecoverWith<TValue, TError>(
		this Result<TValue, TError> result, Func<TValue> functor)
		where TError : Error
		=> result.IsSuccess ? result : new(functor());

	public static async Task<Result<TValue, TError>> RecoverWith<TValue, TError>(
		this Result<TValue, TError> result, Func<Task<TValue>> functor)
		where TError : Error
		=> result.IsSuccess ? result : new(await functor());
	#endregion Functional Closure Extensions
	
	#region Left Async Extensions
	public static async Task<Result<TValue, TError>> RecoverWith<TValue, TError>(
		this Task<Result<TValue, TError>> result, Func<TError, TValue> functor)
		where TError : Error
		=> (await result).RecoverWith(functor);

	public static async Task<Result<TValue, TError>> RecoverWith<TValue, TError>(
		this Task<Result<TValue, TError>> result, Func<TError, Task<TValue>> functor)
		where TError : Error
		=> await (await result).RecoverWith(functor);

	public static async Task<Result<TValue, TError>> RecoverWith<TValue, TError>(
		this Task<Result<TValue, TError>> result, TValue newValue)
		where TError : Error
		=> (await result).RecoverWith(newValue);

	public static async Task<Result<TValue, TError>> RecoverWith<TValue, TError>(
		this Task<Result<TValue, TError>> result, Func<TValue> functor)
		where TError : Error
		=> (await result).RecoverWith(functor);

	public static async Task<Result<TValue, TError>> RecoverWith<TValue, TError>(
		this Task<Result<TValue, TError>> result, Func<Task<TValue>> functor)
		where TError : Error
		=> await (await result).RecoverWith(functor);
	#endregion Left Async Extensions
}