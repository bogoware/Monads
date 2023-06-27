using System.Collections;
using System.Runtime.CompilerServices;

namespace Bogoware.Monads;

public static class Result
{
	public static Result<Unit> Unit { get; } = new(Monads.Unit.Instance);

	/// <summary>
	/// Initializes a new successful instance of the <see cref="Result{TValue}"/> with the given <paramref name="value"/>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Result<TValue> Success<TValue>(TValue value) => new(value);

	/// <summary>
	/// Initializes a new failed instance of the <see cref="Result{TValue}"/> with a <see cref="LogicError"/>
	/// with the message <paramref name="errorMessage"/>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Result<TValue> Failure<TValue>(string errorMessage) => new(new LogicError(errorMessage));

	/// <summary>
	/// Initializes a new failed instance of the <see cref="Result{TValue}"/> with the given <paramref name="error"/>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Result<TValue> Failure<TValue>(Error error) => new(error);

	/// <summary>
	///  If the <paramref name="condition"/> is <c>true</c> then initializes a new successful instance
	/// of <see cref="Result{Unit}"/>, otherwise return a failed instance of <see cref="Result{Unit}"/>
	/// with the given <paramref name="error"/>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Result<Unit> Ensure(bool condition, Func<Error> error) => condition ? Unit : error();
	
	/// <summary>
	///  If the <paramref name="predicate"/> evaluates to <c>true</c> then initializes a new successful instance
	/// of <see cref="Result{Unit}"/>, otherwise return a failed instance of <see cref="Result{Unit}"/>
	/// with the given <paramref name="error"/>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Result<Unit> Ensure(Func<bool> predicate, Func<Error> error) => predicate() ? Unit : error();
	
	/// <summary>
	///  If the <paramref name="predicate"/> evaluates to <c>true</c> then initializes a new successful instance
	/// of <see cref="Result{Unit}"/>, otherwise return a failed instance of <see cref="Result{Unit}"/>
	/// with the given <paramref name="error"/>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Result<Unit>> Ensure(Func<Task<bool>> predicate, Func<Error> error) => await predicate() ? Unit : error();

	/// <summary>
	/// Initializes a new instance of the <see cref="Result{TValue}"/> with the value returned by <paramref name="result"/>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Result<TValue> Bind<TValue>(Func<Result<TValue>> result) => result();

	/// <summary>
	/// Initializes a new instance of the <see cref="Result{TValue}"/> with the value returned by <paramref name="result"/>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Task<Result<TValue>> Bind<TValue>(Func<Task<Result<TValue>>> result) => result();


	/// <summary>
	/// Wraps the execution of the given <paramref name="action"/> in a <see cref="Result{TValue}"/>
	/// catching any thrown exception and returning it as an <see cref="RuntimeError"/> .
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Result<Unit> Execute(Action action)
	{
		RuntimeError? error = null;
		try
		{
			action();
		}
		catch (Exception ex)
		{
			error = new(ex);
		}

		return error ?? Unit;
	}

	/// <summary>
	///	Wraps the execution of the given <paramref name="action"/> in a <see cref="Result{TValue}"/>
	/// catching any thrown exception and returning it as an <see cref="RuntimeError"/> .
	/// </summary>
	public static async Task<Result<Unit>> Execute(Func<Task> action)
	{
		RuntimeError? error = null;
		try
		{
			await action();
		}
		catch (Exception ex)
		{
			error = new(ex);
		}

		return error ?? Unit;
	}

	/// <summary>
	/// Wraps the execution of the given <paramref name="function"/> in a <see cref="Result{TValue}"/>
	/// catching any thrown exception and returning it as an <see cref="RuntimeError"/> .
	/// </summary>
	public static Result<TValue> Execute<TValue>(Func<TValue> function)
	{
		RuntimeError? error = null;
		TValue? value = default;
		try
		{
			value = function();
		}
		catch (Exception ex)
		{
			error = new(ex);
		}

		return error ?? new Result<TValue>(value!);
	}

	/// <summary>
	/// Wraps the execution of the given <paramref name="function"/> in a <see cref="Result{TValue}"/>
	/// catching any thrown exception and returning it as an <see cref="RuntimeError"/> .
	/// </summary>
	public static async Task<Result<TValue>> Execute<TValue>(Func<Task<TValue>> function)
	{
		RuntimeError? error = null;
		TValue? value = default;
		try
		{
			value = await function();
		}
		catch (Exception ex)
		{
			error = new(ex);
		}

		return error ?? new Result<TValue>(value!);
	}
}

public readonly struct Result<TValue> : IResult<TValue>, IEquatable<Result<TValue>>, IEnumerable<TValue>
{
	private readonly TValue? _value;
	private readonly Error? _error;
	private readonly bool _isSuccess;

	public Result(TValue value) => (_value, _isSuccess) = (value, true);
	public Result(Error error) => (_error, _isSuccess) = (error, false);

	public Result(Result<TValue> result) =>
		(_value, _error, _isSuccess) = (result._value, result._error, result._isSuccess);

	public bool IsSuccess => _isSuccess;
	public bool IsFailure => !_isSuccess;

	public static implicit operator Result<TValue>(TValue value) => new(value);
	public static implicit operator Result<TValue>(Error error) => new(error);

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

	/// <summary>
	/// In case of success returns the <paramref name="newValue"/>..
	/// </summary>
	public Result<TNewValue> Map<TNewValue>(TNewValue newValue)
		=> _value is null ? new(_error!) : new(newValue);

	/// <summary>
	/// In case of success returns the <paramref name="functor"/> result.
	/// </summary>
	public Result<TNewValue> Map<TNewValue>(Func<TNewValue> functor)
		=> _value is null ? new(_error!) : new(functor());

	/// <inheritdoc cref="Map{TNewValue}(System.Func{TNewValue})"/>
	public async Task<Result<TNewValue>> Map<TNewValue>(Func<Task<TNewValue>> functor)
		=> _value is null ? new(_error!) : new(await functor());

	/// <summary>
	/// In case of success transform the original value by applying the <paramref name="functor"/>.
	/// </summary>
	public Result<TNewValue> Map<TNewValue>(Func<TValue, TNewValue> functor)
		=> _value is null ? new(_error!) : new(functor(_value));

	/// <inheritdoc cref="M:Bogoware.Monads.Result`1.Map``1(System.Func{`0,``0})"/>
	public async Task<Result<TNewValue>> Map<TNewValue>(Func<TValue, Task<TNewValue>> functor)
		=> _value is null ? new(_error!) : new(await functor(_value));

	/// <summary>
	/// In case of failure return the <paramref name="newError"/>.
	/// </summary>
	public Result<TValue> MapError(Error newError)
		=> _value is null ? newError : this;

	/// <summary>
	/// In case of failure return the <paramref name="newErrorFunctor"/> result.
	/// </summary>
	public Result<TValue> MapError<TNewError>(Func<TNewError> newErrorFunctor)
		where TNewError : Error
		=> _value is null ? newErrorFunctor() : this;

	/// <inheritdoc cref="MapError(Error)"/>
	public Result<TValue> MapError<TNewError>(Func<Error, TNewError> newErrorFunctor)
		where TNewError : Error
		=> _value is null ? newErrorFunctor(_error!) : this;

	/// <inheritdoc cref="MapError(Error)"/>
	public async Task<Result<TValue>> MapError<TNewError>(Func<Task<TNewError>> newErrorFunctor)
		where TNewError : Error
		=> _value is null ? await newErrorFunctor() : this;

	/// <inheritdoc cref="MapError(Error)"/>
	public async Task<Result<TValue>> MapError<TNewError>(Func<Error, Task<TNewError>> newErrorFunctor)
		where TNewError : Error
		=> _value is null ? await newErrorFunctor(_error!) : this;

	/// <summary>
	/// In case of success return the <paramref name="newResult"/>.
	/// </summary>
	public Result<TNewValue> Bind<TNewValue>(Result<TNewValue> newResult)
		=> _value is null ? new(_error!) : newResult;

	/// <summary>
	/// In case of success return the <paramref name="functor"/> result.
	/// </summary>
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

	public Result<TValue> Ensure(Func<TValue, bool> functor, Func<Maybe<TValue>, Error> errorFunctor)
		=> _value is not null & functor(_value!) ? this : new(errorFunctor(_value));

	public async Task<Result<TValue>> Ensure(Func<TValue, Task<bool>> functor, Func<Maybe<TValue>,Error> errorFunctor)
		=> _value is not null & await functor(_value!) ? this : new(errorFunctor(_value));
	
	public async Task<Result<TValue>> Ensure(Func<TValue, bool> functor, Func<Maybe<TValue>, Task<Error>> errorFunctor)
		=> _value is not null & functor(_value!) ? this : new(await errorFunctor(_value));

	public async Task<Result<TValue>> Ensure(Func<TValue, Task<bool>> functor, Func<Maybe<TValue>,Task<Error>> errorFunctor)
		=> _value is not null & await functor(_value!) ? this : new(await errorFunctor(_value));

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