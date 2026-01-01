using System.Runtime.CompilerServices;
// ReSharper disable UnusedMethodReturnValue.Global
// ReSharper disable UnusedMember.Global

// ReSharper disable MemberCanBePrivate.Global

namespace Bogoware.Monads;

public static class ResultExecuteExtensions
{
    #region Functional Closure Extensions

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<TValue> IfSuccess<TValue>(
        this Result<TValue> result, Action action)
    {
        if (result.IsSuccess) action();
        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static async Task<Result<TValue>> IfSuccess<TValue>(
        this Result<TValue> result, Func<Task> action)
    {
        if (result.IsSuccess) await action();
        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<TValue> IfFailure<TValue>(
        this Result<TValue> result, Action action)
    {
        if (result.IsFailure) action();
        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static async Task<Result<TValue>> IfFailure<TValue>(
        this Result<TValue> result, Func<Task> action)
    {
        if (result.IsFailure) await action();
        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<TValue> Execute<TValue>(this Result<TValue> result, Action action)
    {
        action();
        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static async Task<Result<TValue>> Execute<TValue>(
        this Result<TValue> result, Func<Task> action)
    {
        await action();
        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<TValue> Execute<TValue>(
        this Result<TValue> result, Action<Result<TValue>> action)
    {
        action(result);
        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static async Task<Result<TValue>> Execute<TValue>(
        this Result<TValue> result, Func<Result<TValue>, Task> action)
    {
        await action(result);
        return result;
    }

    #endregion Functional Closure Extensions

    #region Left Async Extensions

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static async Task<Result<TValue>> IfSuccess<TValue>(
        this Task<Result<TValue>> result, Action<TValue> action)
        => (await result).IfSuccess(action);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static async Task<Result<TValue>> IfSuccess<TValue>(
        this Task<Result<TValue>> result, Func<TValue, Task> action)
        => await (await result).IfSuccess(action);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static async Task<Result<TValue>> IfSuccess<TValue>(
        this Task<Result<TValue>> result, Action action)
        => (await result).IfSuccess(action);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static async Task<Result<TValue>> IfSuccess<TValue>(
        this Task<Result<TValue>> result, Func<Task> action)
        => await (await result).IfSuccess(action);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static async Task<Result<TValue>> IfFailure<TValue>(
        this Task<Result<TValue>> result, Action<Error> action)
        => (await result).IfFailure(action);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static async Task<Result<TValue>> IfFailure<TValue>(
        this Task<Result<TValue>> result, Func<Error, Task> action)
        => await (await result).IfFailure(action);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static async Task<Result<TValue>> IfFailure<TValue>(
        this Task<Result<TValue>> result, Action action)
        => (await result).IfFailure(action);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static async Task<Result<TValue>> IfFailure<TValue>(
        this Task<Result<TValue>> result, Func<Task> action)
        => await (await result).IfFailure(action);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static async Task<Result<TValue>> Execute<TValue>(
        this Task<Result<TValue>> result, Action action)
        => (await result).Execute(action);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static async Task<Result<TValue>> Execute<TValue>(
        this Task<Result<TValue>> result, Func<Task> action)
        => await (await result).Execute(action);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static async Task<Result<TValue>> Execute<TValue>(
        this Task<Result<TValue>> resultTask, Action<Result<TValue>> action)
    {
        var result = await resultTask;
        action(result);
        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static async Task<Result<TValue>> Execute<TValue>(
        this Task<Result<TValue>> resultTask, Func<Result<TValue>, Task> action)
    {
        var result = await resultTask;
        await action(result);
        return result;
    }

    #endregion Left Async Extensions
}