using System.Collections;

namespace Bogoware.Monads;

public readonly struct Result<TValue> : IResult<TValue>, IEquatable<Result<TValue>>, IEnumerable<TValue>
{
	private readonly TValue? _value;
	private readonly Error? _error;

	public Result(TValue value) => _value = value;
	public Result(Error error) => _error = error;

	public bool IsSuccess => _value is not null;
	public bool IsFailure => _error is not null;

	/// <summary>
	/// Returns the value if the <see cref="Result{TValue}"/>.<see cref="IsSuccess"/>
	/// otherwise throw an <see cref="ResultFailedException"/>.
	/// This method should be avoided in favor of pure functional composition style.
	/// </summary>
	/// <returns></returns>
	/// <exception cref="ResultFailedException"></exception>
	public TValue GetValueOrThrow() => _value ?? throw new ResultFailedException(_error!);

	/// <summary>
	/// Returns the error if the <see cref="Result{TValue}"/>.<see cref="IsFailure"/>
	/// otherwise throw an <see cref="ResultSuccessException"/>.
	/// This method should be avoided in favor of pure functional composition style.
	/// </summary>
	/// <returns></returns>
	/// <exception cref="ResultFailedException"></exception>
	public Error GetErrorOrThrow() => _error ?? throw new ResultSuccessException();

	public Result<TNewValue> Map<TNewValue>(TNewValue newValue)
		=> _value is null ? new(_error!) : new(newValue);

	public Result<TNewValue> Map<TNewValue>(Func<TNewValue> functor)
		=> _value is null ? new(_error!) : new(functor());

	public async Task<Result<TNewValue>> Map<TNewValue>(Func<Task<TNewValue>> functor)
		=> _value is null ? new(_error!) : new(await functor());

	public Result<TNewValue> Map<TNewValue>(Func<TValue, TNewValue> functor)
		=> _value is null ? new(_error!) : new(functor(_value));

	public async Task<Result<TNewValue>> Map<TNewValue>(Func<TValue, Task<TNewValue>> functor)
		=> _value is null ? new(_error!) : new(await functor(_value));

	public Result<TNewValue> Bind<TNewValue>(Result<TNewValue> newResult)
		=> _value is null ? new(_error!) : newResult;

	public Result<TNewValue> Bind<TNewValue>(Func<Result<TNewValue>> functor)
		=> _value is null ? new(_error!) : functor();

	public Task<Result<TNewValue>> Bind<TNewValue>(Func<Task<Result<TNewValue>>> functor)
		=> _value is null ? Task.FromResult(new Result<TNewValue>(_error!)) : functor();

	public Result<TNewValue> Bind<TNewValue>(Func<TValue, Result<TNewValue>> functor)
		=> _value is null ? new(_error!) : functor(_value);

	public Task<Result<TNewValue>> Bind<TNewValue>(Func<TValue, Task<Result<TNewValue>>> functor)
		=> _value is null ? Task.FromResult(new Result<TNewValue>(_error!)) : functor(_value);

	public TResult Match<TResult>(Func<TValue, TResult> successful, TResult failure)
		=> _value is not null ? successful(_value) : failure;

	public Task<TResult> Match<TResult>(Func<TValue, Task<TResult>> successful, TResult failure)
		=> _value is not null ? successful(_value) : Task.FromResult(failure);

	public TResult Match<TResult>(Func<TValue, TResult> successful, Func<Error, TResult> failure)
		=> _value is not null ? successful(_value) : failure(_error!);

	public Task<TResult> Match<TResult>(Func<TValue, Task<TResult>> successful, Func<Error, TResult> failure)
		=> _value is not null ? successful(_value) : Task.FromResult(failure(_error!));

	public Task<TResult> Match<TResult>(Func<TValue, TResult> successful, Func<Error, Task<TResult>> failure)
		=> _value is not null ? Task.FromResult(successful(_value)) : failure(_error!);

	public Task<TResult> Match<TResult>(Func<TValue, Task<TResult>> successful, Func<Error, Task<TResult>> failure)
		=> _value is not null ? successful(_value) : failure(_error!);

	public Result<TValue> RecoverWith(TValue newValue)
		=> _error is not null ? new(newValue) : this;

	public Result<TValue> RecoverWith(Func<TValue> functor)
		=> _error is not null ? new(functor()) : this;

	public Result<TValue> RecoverWith(Func<Error, TValue> functor)
		=> _error is not null ? new(functor(_error)) : this;

	public async Task<Result<TValue>> RecoverWith(Func<Task<TValue>> functor)
		=> _error is not null ? new(await functor()) : this;

	public async Task<Result<TValue>> RecoverWith(Func<Error, Task<TValue>> functor)
		=> _error is not null ? new(await functor(_error)) : this;

	public Result<TValue> Ensure(Func<TValue, bool> functor, Error error)
		=> _value is not null & functor(_value!) ? this : new(error);

	public async Task<Result<TValue>> Ensure(Func<TValue, Task<bool>> functor, Error error)
		=> _value is not null & await functor(_value!) ? this : new(error);


	/// <summary>
	/// Execute the action if the <see cref="Result{TValue}"/>.<see cref="IsSuccess"/> is true.
	/// </summary>
	public Result<TValue> ExecuteIfSuccess(Action<TValue> action)
	{
		if (_value is not null) action(_value);
		return this;
	}

	/// <inheritdoc cref="ExecuteIfSuccess(System.Action{TValue})"/>
	public async Task<Result<TValue>> ExecuteIfSuccess(Func<TValue, Task> action)
	{
		if (_value is not null) await action(_value);
		return this;
	}

	/// <summary>
	/// Execute the action if the <see cref="Result{TValue}"/>.<see cref="IsFailure"/> is true.
	/// </summary>
	public Result<TValue> ExecuteIfFailure(Action<Error> action)
	{
		if (_error is not null) action(_error);
		return this;
	}


	/// <inheritdoc cref="ExecuteIfFailure(System.Action{Error})"/>
	public async Task<Result<TValue>> ExecuteIfFailure(Func<Error, Task> action)
	{
		if (_error is not null) await action(_error);
		return this;
	}

	public bool Equals(Result<TValue> other)
		=> EqualityComparer<TValue?>.Default.Equals(_value, other._value)
		   && EqualityComparer<Error?>.Default.Equals(_error, other._error);

	public IEnumerator<TValue> GetEnumerator()
	{
		if (_value is not null) yield return _value;
	}

	public override bool Equals(object? obj) => obj is Result<TValue> other && Equals(other);

	public override int GetHashCode() => HashCode.Combine(_value, _error);
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public static bool operator ==(Result<TValue> left, Result<TValue> right) => left.Equals(right);

	public static bool operator !=(Result<TValue> left, Result<TValue> right) => !left.Equals(right);
}