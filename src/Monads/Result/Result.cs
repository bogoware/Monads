using System.Diagnostics;

namespace Bogoware.Monads;

public readonly struct Result<TValue, TError> : IResult
	where TError : Error
{
	private readonly TValue? _value;
	private readonly TError? _error;

	public Result(TValue value) => _value = value;
	public Result(TError error) => _error = error;

	public bool IsSuccess => _value is not null;
	public bool IsFailure => _error is not null;

	/// <summary>
	/// Returns the value if the <see cref="Result{TValue,TError}"/>.<see cref="IsSuccess"/>
	/// otherwise throw an <see cref="ResultFailedException"/>.
	/// This method should be avoided in favor of pure functional composition style.
	/// </summary>
	/// <returns></returns>
	/// <exception cref="ResultFailedException"></exception>
	public TValue GetValueOrThrow() => _value ?? throw new ResultFailedException(_error!);
	
	/// <summary>
	/// Returns the error if the <see cref="Result{TValue,TError}"/>.<see cref="IsFailure"/>
	/// otherwise throw an <see cref="ResultSuccessException"/>.
	/// This method should be avoided in favor of pure functional composition style.
	/// </summary>
	/// <returns></returns>
	/// <exception cref="ResultFailedException"></exception>
	public TError GetErrorOrThrow() => _error ?? throw new ResultSuccessException();

	public Result<TNewValue, TError> Map<TNewValue>(TNewValue newValue)
		=> _value is null ? new(_error!) : new(newValue);

	public Result<TNewValue, TError> Map<TNewValue>(Func<TNewValue> functor)
		=> _value is null ? new(_error!) : new(functor());

	public async Task<Result<TNewValue, TError>> Map<TNewValue>(Func<Task<TNewValue>> functor)
		=> _value is null ? new(_error!) : new(await functor());

	public Result<TNewValue, TError> Map<TNewValue>(Func<TValue, TNewValue> functor)
		=> _value is null ? new(_error!) : new(functor(_value));

	public async Task<Result<TNewValue, TError>> Map<TNewValue>(Func<TValue, Task<TNewValue>> functor)
		=> _value is null ? new(_error!) : new(await functor(_value));

	public Result<TNewValue, TError> Bind<TNewValue>(Result<TNewValue, TError> newResult)
		=> _value is null ? new(_error!) : newResult;

	public Result<TNewValue, TError> Bind<TNewValue>(Func<Result<TNewValue, TError>> functor)
		=> _value is null ? new(_error!) : functor();

	public Task<Result<TNewValue, TError>> Bind<TNewValue>(Func<Task<Result<TNewValue, TError>>> functor)
		=> _value is null ? Task.FromResult(new Result<TNewValue, TError>(_error!)) : functor();

	public Result<TNewValue, TError> Bind<TNewValue>(Func<TValue, Result<TNewValue, TError>> functor)
		=> _value is null ? new(_error!) : functor(_value);

	public Task<Result<TNewValue, TError>> Bind<TNewValue>(Func<TValue, Task<Result<TNewValue, TError>>> functor)
		=> _value is null ? Task.FromResult(new Result<TNewValue, TError>(_error!)) : functor(_value);

	public TResult Match<TResult>(Func<TValue, TResult> successful, Func<TError, TResult> failure)
		=> _value is not null ? successful(_value) : failure(_error!);

	public Task<TResult> Match<TResult>(Func<TValue, Task<TResult>> successful, Func<TError, TResult> failure)
		=> _value is not null ? successful(_value) : Task.FromResult(failure(_error!));

	public Task<TResult> Match<TResult>(Func<TValue, TResult> successful, Func<TError, Task<TResult>> failure)
		=> _value is not null ? Task.FromResult(successful(_value)) : failure(_error!);

	public Task<TResult> Match<TResult>(Func<TValue, Task<TResult>> successful, Func<TError, Task<TResult>> failure)
		=> _value is not null ? successful(_value) : failure(_error!);

	public Result<TValue, TError> RecoverWith(Func<TError, TValue> functor)
		=> _error is not null ? new(functor(_error)) : this;

	public async Task<Result<TValue, TError>> RecoverWith(Func<TError, Task<TValue>> functor)
		=> _error is not null ? new(await functor(_error)) : this;

	public Result<TValue, TError> Ensure(Func<TValue, bool> functor, TError error)
		=> _value is not null & functor(_value!) ? this : new(error);

	public async Task<Result<TValue, TError>> Ensure(Func<TValue, Task<bool>> functor, TError error)
		=> _value is not null & await functor(_value!) ? this : new(error);


	// TODO: IfSuccess, IfFailure, Execute
}