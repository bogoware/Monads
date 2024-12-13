using System.Collections;
using System.Runtime.CompilerServices;

// ReSharper disable UnusedMember.Global

// ReSharper disable ArrangeObjectCreationWhenTypeNotEvident

namespace Bogoware.Monads;

public static class Maybe
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Maybe<T> From<T>(T? value) => new(value);

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static Maybe<T> From<T>(T? value) where T : struct => new(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Maybe<T> From<T>(Maybe<T> maybe) => new(maybe);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Maybe<T> Some<T>(T value)
    {
        if (value is null) throw new ArgumentNullException(nameof(value));
        return new(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Maybe<T> Some<T>(Maybe<T> maybe)
    {
        if (maybe.IsNone) throw new MaybeNoneException();

        return new(maybe);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Maybe<T> None<T>() => Maybe<T>.None;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Maybe<Unit> None() => Maybe<Unit>.None;
}


/// <summary>
/// Represents an optional value.
/// </summary>
public readonly struct Maybe<TValue> : IMaybe<TValue>, IEquatable<Maybe<TValue>>, IEnumerable<TValue>
{
    /// <summary>
    /// Is <c>true</c> if the maybe is some, otherwise <c>false</c>.
    /// </summary>
    public bool IsSome => Value is not null;
    /// <summary>
    /// Is <c>true</c> if the maybe is none, otherwise <c>false</c>.
    /// </summary>
    public bool IsNone => Value is null;
    /// <summary>
    /// Returns the singleton instance of <see cref="Maybe{T}"/> representing the none state.
    /// </summary>
    public static readonly Maybe<TValue> None = default;

    /// <summary>
    /// Gets the value if the maybe is some, otherwise the <c>default</c>.
    /// </summary>
    public TValue? Value { get; } = default;

    /// <summary>
    /// Initializes a new instance of the <see cref="Maybe{T}"/>.
    /// </summary>
    public Maybe(TValue? value)
    {
        if (value is not null)
        {
            Value = value;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Maybe{T}"/>.
    /// </summary>
    public Maybe(Maybe<TValue> maybe) => Value = maybe.Value;

    /// <summary>
    /// Returns the value if the maybe is some, otherwise throws an exception.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="MaybeNoneException"></exception>
    public TValue GetValueOrThrow() => Value ?? throw new MaybeNoneException();

    /// <summary>
    /// Map the value to a new one.
    /// </summary>
    public Maybe<TNewValue> Map<TNewValue>(Func<TValue, TNewValue?> map) where TNewValue : class
        => Value is not null ? new Maybe<TNewValue>(map(Value)) : Maybe<TNewValue>.None;
    
    

    /// <inheritdoc cref="M:Bogoware.Monads.Maybe`1.Map``1(System.Func{`0,``0})"/>
    public async Task<Maybe<TNewValue>> Map<TNewValue>(Func<TValue, Task<TNewValue?>> map) where TNewValue : class
        => Value is not null ? new(await map(Value)) : Maybe<TNewValue>.None;

    /// <summary>
    /// Bind the maybe and, possibly, to a new one.
    /// </summary> 
    public Maybe<TNewValue> Bind<TNewValue>(Func<TValue, Maybe<TNewValue>> map) where TNewValue : class
        => Value is not null ? map(Value) : Maybe<TNewValue>.None;

    /// <inheritdoc cref="M:Bogoware.Monads.Maybe`1.Bind``1(System.Func{`0,Bogoware.Monads.Maybe{``0}})"/>
    public Task<Maybe<TNewValue>> Bind<TNewValue>(Func<TValue, Task<Maybe<TNewValue>>> map) where TNewValue : class
        => Value is not null ? map(Value) : Task.FromResult(Maybe<TNewValue>.None);

    /// <summary>
    /// Maps a new value for both state of a <see cref="Maybe{T}"/> 
    /// </summary>
    public TResult Match<TResult>(Func<TValue, TResult> mapValue, TResult none)
        => Value is not null ? mapValue(Value) : none;

    /// <inheritdoc cref="M:Bogoware.Monads.Maybe`1.Match``1(System.Func{`0,``0},``0)"/>
    public TResult Match<TResult>(Func<TValue, TResult> mapValue, Func<TResult> none)
        => Value is not null ? mapValue(Value) : none();

    /// <inheritdoc cref="M:Bogoware.Monads.Maybe`1.Match``1(System.Func{`0,``0},``0)"/>
    public Task<TResult> Match<TResult>(Func<TValue, Task<TResult>> mapValue, TResult none)
        => Value is not null ? mapValue(Value) : Task.FromResult(none);

    /// <inheritdoc cref="M:Bogoware.Monads.Maybe`1.Match``1(System.Func{`0,``0},``0)"/>
    public Task<TResult> Match<TResult>(Func<TValue, Task<TResult>> mapValue, Func<TResult> none)
        => Value is not null ? mapValue(Value) : Task.FromResult(none());

    /// <inheritdoc cref="M:Bogoware.Monads.Maybe`1.Match``1(System.Func{`0,``0},``0)"/>
    public Task<TResult> Match<TResult>(Func<TValue, Task<TResult>> mapValue, Func<Task<TResult>> none)
        => Value is not null ? mapValue(Value) : none();

    /// <inheritdoc cref="M:Bogoware.Monads.Maybe`1.Match``1(System.Func{`0,``0},``0)"/>
    public Task<TResult> Match<TResult>(Func<TValue, TResult> mapValue, Func<Task<TResult>> none)
        => Value is not null ? Task.FromResult(mapValue(Value)) : none();

    /// <summary>
    /// Execute the action if the <see cref="Maybe{T}"/> is <c>Some</c>.
    /// </summary>
    public Maybe<TValue> IfSome(Action<TValue> action)
    {
        if (Value is not null) action(Value);
        return this;
    }

    /// <inheritdoc cref="M:Bogoware.Monads.Maybe`1.IfSome(System.Action{`0})"/>
    public async Task<Maybe<TValue>> IfSome(Func<TValue, Task> action)
    {
        if (Value is not null) await action(Value);
        return this;
    }

    /// <summary>
    /// Retrieve the value if present or return the <c>defaultValue</c> if missing.
    /// </summary>
    public TValue GetValue(TValue defaultValue)
    {
        if (defaultValue is null) throw new ArgumentNullException(nameof(defaultValue));

        return Value ?? defaultValue;
    }

    /// <inheritdoc cref="GetValue(TValue)"/>
    public TValue GetValue(Func<TValue> defaultValue)
    {
        if (defaultValue is null) throw new ArgumentNullException(nameof(defaultValue));

        return Value ?? defaultValue();
    }

    /// <inheritdoc cref="GetValue(TValue)"/>
    public async Task<TValue> GetValue(Func<Task<TValue>> defaultValue)
    {
        if (defaultValue is null) throw new ArgumentNullException(nameof(defaultValue));
        return Value ?? await defaultValue();
    }

    /// <summary>
    /// Downcast to <c>TNew</c> if possible, otherwise returns a <see cref="Maybe{TNew}"/>
    /// that is actually None case.
    /// </summary>
    /// <typeparam name="TNewValue"></typeparam>
    /// <returns></returns>
    public Maybe<TNewValue> OfType<TNewValue>() where TNewValue : class =>
        typeof(TValue).IsAssignableFrom(typeof(TNewValue))
            ? new Maybe<TNewValue>(Value as TNewValue)
            : Maybe<TNewValue>.None;

    IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
    {
        if (Value is not null) yield return Value;
    }

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<TValue>)this).GetEnumerator();

    public override bool Equals(object? obj)
    {
        if (obj is Maybe<TValue> other) return Equals(other);
        return false;
    }

    public bool Equals(Maybe<TValue> other)
    {
        if (Value is not null) return Value?.Equals(other.Value) ?? false;
        return other.Value is null;
    }

    public override int GetHashCode() => Value?.GetHashCode() ?? 0;

    public static bool operator ==(Maybe<TValue> left, Maybe<TValue> right) => left.Equals(right);

    public static bool operator !=(Maybe<TValue> left, Maybe<TValue> right) => !left.Equals(right);

    public override string ToString() =>
        Value is null ? $"None<{typeof(TValue).GetFriendlyTypeName()}>()" : $"Some({Value})";

    public static implicit operator Maybe<TValue>(TValue? value) => new(value);
}