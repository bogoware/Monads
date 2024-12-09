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
public struct Result<TValue> : IResult<TValue>, IEquatable<Result<TValue>>, IEnumerable<TValue>
{
    internal readonly TValue? Value;
    internal readonly Error? Error;

    /// <summary>
    /// Initializes a successful instance of the <see cref="Result{TValue}"/> with the given <paramref name="value"/>.
    /// </summary>
    public Result(TValue value) => (Value, IsSuccess) = (value, true);

    /// <summary>
    /// Initializes a failed instance of the <see cref="Result{TValue}"/> with the given <paramref name="error"/>.
    /// </summary>
    /// <param name="error"></param>
    public Result(Error error) => (Error, IsSuccess) = (error, false);

    public Result(Result<TValue> result) =>
        (Value, Error, IsSuccess) = (result.Value, result.Error, result.IsSuccess);

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

    /// <summary>
    /// Returns the value if the <see cref="Result{TValue}"/>.<see cref="IsSuccess"/>
    /// otherwise throw an <see cref="ResultFailedException"/>.
    /// This method should be avoided in favor of pure functional composition style.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ResultFailedException"></exception>
    public TValue GetValueOrThrow() => IsSuccess ? Value! : throw new ResultFailedException(Error!);

    /// <summary>
    /// Returns the error if the <see cref="Result{TValue}"/>.<see cref="IsFailure"/>
    /// otherwise throw an <see cref="ResultSuccessException"/>.
    /// This method should be avoided in favor of pure functional composition style.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ResultFailedException"></exception>
    public Error GetErrorOrThrow() => IsFailure ? Error! : throw new ResultSuccessException();

    /// <summary>
    /// In case of success returns the <paramref name="newValue"/>..
    /// </summary>
    public Result<TNewValue> Map<TNewValue>(TNewValue newValue)
        => IsSuccess ? newValue : Error!;

    /// <summary>
    /// In case of success returns the <paramref name="functor"/> result.
    /// </summary>
    public Result<TNewValue> Map<TNewValue>(Func<TNewValue> functor)
        => IsSuccess ? functor() : Error!;

    /// <inheritdoc cref="Map{TNewValue}(System.Func{TNewValue})"/>
    public async Task<Result<TNewValue>> Map<TNewValue>(Func<Task<TNewValue>> functor)
        => IsSuccess ? await functor() : Error!;

    /// <summary>
    /// In case of success transform the original value by applying the <paramref name="functor"/>.
    /// </summary>
    public Result<TNewValue> Map<TNewValue>(Func<TValue, TNewValue> functor)
        => IsSuccess ? functor(Value!) : Error!;

    /// <inheritdoc cref="M:Bogoware.Monads.Result`1.Map``1(System.Func{`0,``0})"/>
    public async Task<Result<TNewValue>> Map<TNewValue>(Func<TValue, Task<TNewValue>> functor)
        => IsSuccess ? await functor(Value!) : Error!;

    /// <inheritdoc cref="M:Bogoware.Monads.Result`1.Map``1(System.Func{`0,``0})"/>
    public Result<Unit> Map(Action<TValue> functor)
    {
        if (IsFailure) return Error!;
        functor(Value!);
        return Result.Unit;
    }

    /// <inheritdoc cref="M:Bogoware.Monads.Result`1.Map``1(System.Func{`0,``0})"/>
    public async Task<Result<Unit>> Map(Func<TValue, Task> functor)
    {
        if (IsFailure) return new Result<Unit>(Error!);
        await functor(Value!);
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
        => IsSuccess ? this : newErrorFunctor(Error!);

    /// <inheritdoc cref="MapError(Monads.Error)"/>
    public async Task<Result<TValue>> MapError<TNewError>(Func<Task<TNewError>> newErrorFunctor)
        where TNewError : Error
        => IsSuccess ? this : await newErrorFunctor();

    /// <inheritdoc cref="MapError(Monads.Error)"/>
    public async Task<Result<TValue>> MapError<TNewError>(Func<Error, Task<TNewError>> newErrorFunctor)
        where TNewError : Error
        => IsSuccess ? this : await newErrorFunctor(Error!);

    /// <summary>
    /// In case of success return the <paramref name="newResult"/>.
    /// </summary>
    public Result<TNewValue> Bind<TNewValue>(Result<TNewValue> newResult)
        => IsSuccess ? newResult : Error!;

    /// <summary>
    /// In case of success return the <paramref name="functor"/> result.
    /// </summary>
    public Result<TNewValue> Bind<TNewValue>(Func<Result<TNewValue>> functor)
        => IsSuccess ? functor() : Error!;

    /// <inheritdoc cref="T:Bogoware.Monads.Result`1"/>
    public Task<Result<TNewValue>> Bind<TNewValue>(Func<Task<Result<TNewValue>>> functor)
        => IsSuccess ? functor() : Task.FromResult(new Result<TNewValue>(Error!));

    /// <inheritdoc cref="T:Bogoware.Monads.Result`1"/>
    public Result<TNewValue> Bind<TNewValue>(Func<TValue, Result<TNewValue>> functor)
        => IsSuccess ? functor(Value!) : Error!;

    /// <inheritdoc cref="T:Bogoware.Monads.Result`1"/>
    public Task<Result<TNewValue>> Bind<TNewValue>(Func<TValue, Task<Result<TNewValue>>> functor)
        => IsSuccess ? functor(Value!) : Task.FromResult(new Result<TNewValue>(Error!));

    /// <summary>
    /// In case of success evaluate the <paramref name="successful"/> functor,  otherwise returns <paramref name="failure"/>.
    /// </summary>
    public TResult Match<TResult>(Func<TValue, TResult> successful, TResult failure)
        => IsSuccess ? successful(Value!) : failure;

    /// <inheritdoc cref="M:Bogoware.Monads.Result`1.Match``1(System.Func{`0,``0},``0)"/>
    public Task<TResult> Match<TResult>(Func<TValue, Task<TResult>> successful, TResult failure)
        => IsSuccess ? successful(Value!) : Task.FromResult(failure);

    /// <inheritdoc cref="M:Bogoware.Monads.Result`1.Match``1(System.Func{`0,``0},``0)"/>
    public TResult Match<TResult>(Func<TValue, TResult> successful, Func<Error, TResult> failure)
        => IsSuccess ? successful(Value!) : failure(Error!);

    /// <inheritdoc cref="M:Bogoware.Monads.Result`1.Match``1(System.Func{`0,``0},``0)"/>
    public Task<TResult> Match<TResult>(Func<TValue, Task<TResult>> successful, Func<Error, TResult> failure)
        => IsSuccess ? successful(Value!) : Task.FromResult(failure(Error!));

    /// <inheritdoc cref="M:Bogoware.Monads.Result`1.Match``1(System.Func{`0,``0},``0)"/>
    public Task<TResult> Match<TResult>(Func<TValue, TResult> successful, Func<Error, Task<TResult>> failure)
        => IsSuccess ? Task.FromResult(successful(Value!)) : failure(Error!);

    /// <inheritdoc cref="M:Bogoware.Monads.Result`1.Match``1(System.Func{`0,``0},``0)"/>
    public Task<TResult> Match<TResult>(Func<TValue, Task<TResult>> successful, Func<Error, Task<TResult>> failure)
        => IsSuccess ? successful(Value!) : failure(Error!);

    public Result<TValue> RecoverWith(TValue newValue)
        => IsSuccess ? this : newValue;

    public Result<TValue> RecoverWith(Func<TValue> functor)
        => IsSuccess ? this : functor();

    public Result<TValue> RecoverWith(Func<Error, TValue> functor)
        => IsSuccess ? this : functor(Error!);

    public async Task<Result<TValue>> RecoverWith(Func<Task<TValue>> functor)
        => IsSuccess ? this : await functor();

    public async Task<Result<TValue>> RecoverWith(Func<Error, Task<TValue>> functor)
        => IsSuccess ? this : await functor(Error!);

    /// <summary>
    /// If the <see cref="Result{TValue}"/>.<see cref="IsSuccess"/> is true then evaluate the <paramref name="predicate"/>
    /// and return the <see cref="Result{TValue}"/> if the predicate is true, otherwise return
    /// a new <see cref="Result{TValue}"/> provided by <paramref name="error"/>.
    /// </summary>
    public Result<TValue> Ensure(Func<TValue, bool> predicate, Error error)
        => IsFailure ? this
            : predicate(Value!) ? this : new Result<TValue>(error);

    /// <inheritdoc cref="Ensure(System.Func{TValue, bool}, Monads.Error)"/>
    public async Task<Result<TValue>> Ensure(Func<TValue, Task<bool>> predicate, Error error)
        => IsFailure ? this
            : await predicate(Value!) ? this : new Result<TValue>(error);

    /// <inheritdoc cref="Ensure(System.Func{TValue, bool}, Monads.Error)"/>
    public Result<TValue> Ensure(Func<TValue, bool> predicate, Func<TValue, Error> error)
        => IsFailure ? this
            : predicate(Value!) ? this : new Result<TValue>(error(Value!));

    /// <inheritdoc cref="Ensure(System.Func{TValue, bool}, Monads.Error)"/>
    public async Task<Result<TValue>> Ensure(Func<TValue, Task<bool>> predicate, Func<TValue, Error> error)
        => IsFailure ? this
            : await predicate(Value!) ? this : new Result<TValue>(error(Value!));

    /// <inheritdoc cref="Ensure(System.Func{TValue, bool}, Monads.Error)"/>
    public async Task<Result<TValue>> Ensure(Func<TValue, bool> predicate, Func<TValue, Task<Error>> error)
        => IsFailure ? this
            : predicate(Value!) ? this : new Result<TValue>(await error(Value!));

    /// <inheritdoc cref="Ensure(System.Func{TValue, bool}, Monads.Error)"/>
    public async Task<Result<TValue>> Ensure(Func<TValue, Task<bool>> predicate, Func<TValue, Task<Error>> error)
        => IsFailure ? this
            : await predicate(Value!) ? this : new Result<TValue>(await error(Value!));

    /// <summary>
    /// Execute the action if the <see cref="Result{TValue}"/>.<see cref="IsSuccess"/> is true.
    /// </summary>
    public Result<TValue> ExecuteIfSuccess(Action<TValue> action)
    {
        if (IsSuccess) action(Value!);
        return this;
    }

    /// <inheritdoc cref="ExecuteIfSuccess(System.Action{TValue})"/>
    public async Task<Result<TValue>> ExecuteIfSuccess(Func<TValue, Task> action)
    {
        if (IsSuccess) await action(Value!);
        return this;
    }

    /// <summary>
    /// Execute the action if the <see cref="Result{TValue}"/>.<see cref="IsFailure"/> is true.
    /// </summary>
    public Result<TValue> ExecuteIfFailure(Action<Error> action)
    {
        if (IsFailure) action(Error!);
        return this;
    }


    /// <inheritdoc cref="ExecuteIfFailure(System.Action{Monads.Error})"/>
    public async Task<Result<TValue>> ExecuteIfFailure(Func<Error, Task> action)
    {
        if (IsFailure) await action(Error!);
        return this;
    }

    public bool Equals(Result<TValue> other)
        => EqualityComparer<TValue?>.Default.Equals(Value, other.Value)
           && EqualityComparer<Error?>.Default.Equals(Error, other.Error);

    public IEnumerator<TValue> GetEnumerator()
    {
        if (IsSuccess) yield return Value!;
    }

    public override bool Equals(object? obj) => obj is Result<TValue> other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(Value, Error);
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public static bool operator ==(Result<TValue> left, Result<TValue> right) => left.Equals(right);

    public static bool operator !=(Result<TValue> left, Result<TValue> right) => !left.Equals(right);
}