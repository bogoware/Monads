using System.Collections;
using System.Diagnostics.CodeAnalysis;
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
    public static async Task<Result<Unit>> Ensure(Func<Task<bool>> predicate, Func<Error> error) =>
        await predicate() ? Unit : error();

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
    /// Initializes a new instance of the <see cref="Result{TValue}"/> with the value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<T> From<T>(T value)
    {
        if (value is Error error) return Result.Failure<T>(error);
        return Result.Success(value);
    } 

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
            error = new RuntimeError(ex);
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
            error = new RuntimeError(ex);
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
            error = new RuntimeError(ex);
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
            error = new RuntimeError(ex);
        }

        return error ?? new Result<TValue>(value!);
    }
}

/// <summary>
/// Represents the result of an operation that may fail.
/// </summary>
/// <typeparam name="TValue"></typeparam>
public readonly struct Result<TValue> : IResult<TValue>, IEquatable<Result<TValue>>, IEnumerable<TValue>
{
    private readonly TValue? _value;
    private readonly Error? _error;

    /// <summary>
    /// Returns the value if the <see cref="Result{TValue}"/>.<see cref="IsSuccess"/>
    /// otherwise throw an <see cref="ResultFailedException"/>.
    /// This method should be avoided in favor of pure functional composition style.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ResultFailedException"></exception>
    public TValue? Value
    {
        get
        {
            if (IsFailure) throw new ResultFailedException(Error!);
            return _value;
        }
    }

    /// <summary>
    /// Returns the error if the <see cref="Result{TValue}"/>.<see cref="IsFailure"/>
    /// otherwise throw an <see cref="ResultSuccessException"/>.
    /// This method should be avoided in favor of pure functional composition style.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ResultFailedException"></exception>
    public Error? Error
    {
        get
        {
            if (IsSuccess) throw new ResultSuccessException();
            return _error;
        }
    }

    /// <summary>
    /// Initializes a successful instance of the <see cref="Result{TValue}"/> with the given <paramref name="value"/>.
    /// </summary>
    public Result(TValue value) => 
        (_value, IsSuccess) = (value, true);

    /// <summary>
    /// Initializes a failed instance of the <see cref="Result{TValue}"/> with the given <paramref name="error"/>.
    /// </summary>
    /// <param name="error"></param>
    public Result(Error error) => (_error, IsSuccess) = (error, false);

    public Result(Result<TValue> result) =>
        (_value, _error, IsSuccess) = (result._value, result._error, result.IsSuccess);

    /// <summary>
    /// Is <c>true</c> if the <see cref="Result{TValue}"/> is successful, otherwise <c>false</c>.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Is <c>true</c> if the <see cref="Result{TValue}"/> is failed, otherwise <c>false</c>.
    /// </summary>
    public bool IsFailure => !IsSuccess;

    public static implicit operator Result<TValue>(TValue value) => new(value);
    public static implicit operator Result<TValue>(Error error) => new(error);

    /// <inheritdoc cref="Value"/>
    public TValue GetValueOrThrow() => Value!;

    /// <inheritdoc cref="Error"/>
    public Error GetErrorOrThrow() => Error!;

    /// <summary>
    /// In case of success returns the <paramref name="newValue"/>..
    /// </summary>
    public Result<TNewValue> Map<TNewValue>(TNewValue newValue)
        => IsSuccess ? newValue : _error!;

    /// <summary>
    /// In case of success returns the <paramref name="functor"/> result.
    /// </summary>
    public Result<TNewValue> Map<TNewValue>(Func<TNewValue> functor)
        => IsSuccess ? functor() : _error!;

    /// <inheritdoc cref="Map{TNewValue}(System.Func{TNewValue})"/>
    public async Task<Result<TNewValue>> Map<TNewValue>(Func<Task<TNewValue>> functor)
        => IsSuccess ? await functor() : _error!;

    /// <summary>
    /// In case of success transform the original value by applying the <paramref name="functor"/>.
    /// </summary>
    public Result<TNewValue> Map<TNewValue>(Func<TValue, TNewValue> functor)
        => IsSuccess ? functor(_value!) : _error!;

    /// <inheritdoc cref="M:Bogoware.Monads.Result`1.Map``1(System.Func{`0,``0})"/>
    public async Task<Result<TNewValue>> Map<TNewValue>(Func<TValue, Task<TNewValue>> functor)
        => IsSuccess ? await functor(_value!) : _error!;

    /// <inheritdoc cref="M:Bogoware.Monads.Result`1.Map``1(System.Func{`0,``0})"/>
    public Result<Unit> Map(Action<TValue> functor)
    {
        if (IsFailure) return _error!;
        functor(_value!);
        return Result.Unit;
    }

    /// <inheritdoc cref="M:Bogoware.Monads.Result`1.Map``1(System.Func{`0,``0})"/>
    public async Task<Result<Unit>> Map(Func<TValue, Task> functor)
    {
        if (IsFailure) return new Result<Unit>(_error!);
        await functor(_value!);
        return Result.Unit;
    }

    /// <summary>
    /// In case of failure return the <paramref name="newError"/>.
    /// </summary>
    public Result<TValue> MapError(Error newError)
        => IsSuccess ? this : newError;

    /// <summary>
    /// In case of failure return the <paramref name="newErrorFunctor"/> result.
    /// </summary>
    public Result<TValue> MapError<TNewError>(Func<TNewError> newErrorFunctor)
        where TNewError : Error
        => IsSuccess ? this : newErrorFunctor();

    /// <inheritdoc cref="MapError(Monads.Error)"/>
    public Result<TValue> MapError<TNewError>(Func<Error, TNewError> newErrorFunctor)
        where TNewError : Error
        => IsSuccess ? this : newErrorFunctor(_error!);

    /// <inheritdoc cref="MapError(Monads.Error)"/>
    public async Task<Result<TValue>> MapError<TNewError>(Func<Task<TNewError>> newErrorFunctor)
        where TNewError : Error
        => IsSuccess ? this : await newErrorFunctor();

    /// <inheritdoc cref="MapError(Monads.Error)"/>
    public async Task<Result<TValue>> MapError<TNewError>(Func<Error, Task<TNewError>> newErrorFunctor)
        where TNewError : Error
        => IsSuccess ? this : await newErrorFunctor(_error!);

    /// <summary>
    /// In case of success return the <paramref name="newResult"/>.
    /// </summary>
    public Result<TNewValue> Bind<TNewValue>(Result<TNewValue> newResult)
        => IsSuccess ? newResult : _error!;

    /// <summary>
    /// In case of success return the <paramref name="functor"/> result.
    /// </summary>
    public Result<TNewValue> Bind<TNewValue>(Func<Result<TNewValue>> functor)
        => IsSuccess ? functor() : _error!;

    /// <inheritdoc cref="T:Bogoware.Monads.Result`1"/>
    public Task<Result<TNewValue>> Bind<TNewValue>(Func<Task<Result<TNewValue>>> functor)
        => IsSuccess ? functor() : Task.FromResult(new Result<TNewValue>(_error!));

    /// <inheritdoc cref="T:Bogoware.Monads.Result`1"/>
    public Result<TNewValue> Bind<TNewValue>(Func<TValue, Result<TNewValue>> functor)
        => IsSuccess ? functor(_value!) : _error!;

    /// <inheritdoc cref="T:Bogoware.Monads.Result`1"/>
    public Task<Result<TNewValue>> Bind<TNewValue>(Func<TValue, Task<Result<TNewValue>>> functor)
        => IsSuccess ? functor(_value!) : Task.FromResult(new Result<TNewValue>(_error!));

    /// <summary>
    /// In case of success evaluate the <paramref name="successful"/> functor,  otherwise returns <paramref name="failure"/>.
    /// </summary>
    public TResult Match<TResult>(Func<TValue, TResult> successful, TResult failure)
        => IsSuccess ? successful(_value!) : failure;

    /// <inheritdoc cref="M:Bogoware.Monads.Result`1.Match``1(System.Func{`0,``0},``0)"/>
    public Task<TResult> Match<TResult>(Func<TValue, Task<TResult>> successful, TResult failure)
        => IsSuccess ? successful(_value!) : Task.FromResult(failure);

    /// <inheritdoc cref="M:Bogoware.Monads.Result`1.Match``1(System.Func{`0,``0},``0)"/>
    public TResult Match<TResult>(Func<TValue, TResult> successful, Func<Error, TResult> failure)
        => IsSuccess ? successful(_value!) : failure(_error!);

    /// <inheritdoc cref="M:Bogoware.Monads.Result`1.Match``1(System.Func{`0,``0},``0)"/>
    public Task<TResult> Match<TResult>(Func<TValue, Task<TResult>> successful, Func<Error, TResult> failure)
        => IsSuccess ? successful(_value!) : Task.FromResult(failure(_error!));

    /// <inheritdoc cref="M:Bogoware.Monads.Result`1.Match``1(System.Func{`0,``0},``0)"/>
    public Task<TResult> Match<TResult>(Func<TValue, TResult> successful, Func<Error, Task<TResult>> failure)
        => IsSuccess ? Task.FromResult(successful(_value!)) : failure(_error!);

    /// <inheritdoc cref="M:Bogoware.Monads.Result`1.Match``1(System.Func{`0,``0},``0)"/>
    public Task<TResult> Match<TResult>(Func<TValue, Task<TResult>> successful, Func<Error, Task<TResult>> failure)
        => IsSuccess ? successful(_value!) : failure(_error!);

    public Result<TValue> RecoverWith(TValue newValue)
        => IsSuccess ? this : newValue;

    public Result<TValue> RecoverWith(Func<TValue> functor)
        => IsSuccess ? this : functor();

    public Result<TValue> RecoverWith(Func<Error, TValue> functor)
        => IsSuccess ? this : functor(_error!);

    public async Task<Result<TValue>> RecoverWith(Func<Task<TValue>> functor)
        => IsSuccess ? this : await functor();

    public async Task<Result<TValue>> RecoverWith(Func<Error, Task<TValue>> functor)
        => IsSuccess ? this : await functor(_error!);

    /// <summary>
    /// If the <see cref="Result{TValue}"/>.<see cref="IsSuccess"/> is true then evaluate the <paramref name="predicate"/>
    /// and return the <see cref="Result{TValue}"/> if the predicate is true, otherwise return
    /// a new <see cref="Result{TValue}"/> provided by <paramref name="error"/>.
    /// </summary>
    public Result<TValue> Ensure(Func<TValue, bool> predicate, Error error)
        => IsFailure ? this
            : predicate(_value!) ? this : new Result<TValue>(error);

    /// <inheritdoc cref="Ensure(System.Func{TValue, bool}, Monads.Error)"/>
    public async Task<Result<TValue>> Ensure(Func<TValue, Task<bool>> predicate, Error error)
        => IsFailure ? this
            : await predicate(_value!) ? this : new Result<TValue>(error);

    /// <inheritdoc cref="Ensure(System.Func{TValue, bool}, Monads.Error)"/>
    public Result<TValue> Ensure(Func<TValue, bool> predicate, Func<TValue, Error> error)
        => IsFailure ? this
            : predicate(_value!) ? this : new Result<TValue>(error(_value!));

    /// <inheritdoc cref="Ensure(System.Func{TValue, bool}, Monads.Error)"/>
    public async Task<Result<TValue>> Ensure(Func<TValue, Task<bool>> predicate, Func<TValue, Error> error)
        => IsFailure ? this
            : await predicate(_value!) ? this : new Result<TValue>(error(_value!));

    /// <inheritdoc cref="Ensure(System.Func{TValue, bool}, Monads.Error)"/>
    public async Task<Result<TValue>> Ensure(Func<TValue, bool> predicate, Func<TValue, Task<Error>> error)
        => IsFailure ? this
            : predicate(_value!) ? this : new Result<TValue>(await error(_value!));

    /// <inheritdoc cref="Ensure(System.Func{TValue, bool}, Monads.Error)"/>
    public async Task<Result<TValue>> Ensure(Func<TValue, Task<bool>> predicate, Func<TValue, Task<Error>> error)
        => IsFailure ? this
            : await predicate(_value!) ? this : new Result<TValue>(await error(_value!));

    /// <summary>
    /// Execute the action if the <see cref="Result{TValue}"/>.<see cref="IsSuccess"/> is true.
    /// </summary>
    public Result<TValue> IfSuccess(Action<TValue> action)
    {
        if (IsSuccess) action(_value!);
        return this;
    }

    /// <inheritdoc cref="M:Bogoware.Monads.Maybe`1.IfSome(System.Action{`0})"/>
    public async Task<Result<TValue>> IfSuccess(Func<TValue, Task> action)
    {
        if (IsSuccess) await action(_value!);
        return this;
    }

    /// <summary>
    /// Execute the action if the <see cref="Result{TValue}"/>.<see cref="IsFailure"/> is true.
    /// </summary>
    public Result<TValue> IfFailure(Action<Error> action)
    {
        if (IsFailure) action(_error!);
        return this;
    }

    /// <inheritdoc cref="M:Bogoware.Monads.Result`1.IfFailure(System.Action{Bogoware.Monads.Error})"/>
    public async Task<Result<TValue>> IfFailure(Func<Error, Task> action)
    {
        if (IsFailure) await action(_error!);
        return this;
    }

    public bool Equals(Result<TValue> other)
        => EqualityComparer<TValue?>.Default.Equals(_value, other._value)
           && EqualityComparer<Error?>.Default.Equals(_error, other._error);

    public IEnumerator<TValue> GetEnumerator()
    {
        if (IsSuccess) yield return Value!;
    }

    public override bool Equals(object? obj) => obj is Result<TValue> other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(_value, _error);
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public static bool operator ==(Result<TValue> left, Result<TValue> right) => left.Equals(right);

    public static bool operator !=(Result<TValue> left, Result<TValue> right) => !left.Equals(right);

    public override string ToString() =>
        IsSuccess
            ? $"Success({_value})"
            : $"Failure<{typeof(TValue).GetFriendlyTypeName()}>({_error!.Message})";
}