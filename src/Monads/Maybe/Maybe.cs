using System.Collections;
using System.Runtime.CompilerServices;

// ReSharper disable UnusedMember.Global

// ReSharper disable ArrangeObjectCreationWhenTypeNotEvident

namespace Bogoware.Monads;

public static class Maybe
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Maybe<T> From<T>(T? value)  => new(value);
	
	//[MethodImpl(MethodImplOptions.AggressiveInlining)]
	//public static Maybe<T> From<T>(T? value) where T : struct => new(value);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Maybe<T> From<T>(Maybe<T> maybe) => new(maybe);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Maybe<T> Some<T>(T value)
	{
		ArgumentNullException.ThrowIfNull(value);
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
	private readonly TValue? _value = default;
	/// <summary>
	/// Is <c>true</c> if the maybe is some, otherwise <c>false</c>.
	/// </summary>
	public bool IsSome => _value is not null;
	/// <summary>
	/// Is <c>true</c> if the maybe is none, otherwise <c>false</c>.
	/// </summary>
	public bool IsNone => _value is null;
	/// <summary>
	/// Returns the singleton instance of <see cref="Maybe{T}"/> representing the none state.
	/// </summary>
	public static readonly Maybe<TValue> None = default;

	/// <summary>
	/// Initializes a new instance of the <see cref="Maybe{T}"/>.
	/// </summary>
	public Maybe(TValue? value)
	{
		if (value is not null)
		{
			_value = value;
		}
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="Maybe{T}"/>.
	/// </summary>
	public Maybe(Maybe<TValue> maybe) => _value = maybe._value;
	
	/// <summary>
	/// Returns the value if the maybe is some, otherwise throws an exception.
	/// </summary>
	/// <returns></returns>
	/// <exception cref="MaybeNoneException"></exception>
	public TValue GetValueOrThrow() => _value ?? throw new MaybeNoneException();

	/// <summary>
	/// Map the value to a new one.
	/// </summary>
	public Maybe<TNewValue> Map<TNewValue>(Func<TValue, TNewValue> map) where TNewValue : class
		=> _value is not null ? new Maybe<TNewValue>(map(_value)) : Maybe<TNewValue>.None;

	/// <inheritdoc cref="M:Bogoware.Monads.Maybe`1.Map``1(System.Func{`0,``0})"/>
	public async Task<Maybe<TNewValue>> Map<TNewValue>(Func<TValue, Task<TNewValue>> map) where TNewValue : class
		=> _value is not null ? new(await map(_value)) : Maybe<TNewValue>.None;
	
	/// <summary>
	/// Bind the maybe and, possibly, to a new one.
	/// </summary> 
	public Maybe<TNewValue> Bind<TNewValue>(Func<TValue, Maybe<TNewValue>> map) where TNewValue : class
		=> _value is not null ? map(_value) : Maybe<TNewValue>.None;
	
	/// <inheritdoc cref="M:Bogoware.Monads.Maybe`1.Bind``1(System.Func{`0,Bogoware.Monads.Maybe{``0}})"/>
	public Task<Maybe<TNewValue>> Bind<TNewValue>(Func<TValue, Task<Maybe<TNewValue>>> map) where TNewValue : class
		=> _value is not null ? map(_value) : Task.FromResult(Maybe<TNewValue>.None);

	/// <summary>
	/// Maps a new value for both state of a <see cref="Maybe{T}"/> 
	/// </summary>
	public TResult Match<TResult>(Func<TValue, TResult> mapValue, TResult none)
		=> _value is not null ? mapValue(_value) : none;

	/// <inheritdoc cref="M:Bogoware.Monads.Maybe`1.Match``1(System.Func{`0,``0},``0)"/>
	public TResult Match<TResult>(Func<TValue, TResult> mapValue, Func<TResult> none)
		=> _value is not null ? mapValue(_value) : none();

	/// <inheritdoc cref="M:Bogoware.Monads.Maybe`1.Match``1(System.Func{`0,``0},``0)"/>
	public Task<TResult> Match<TResult>(Func<TValue, Task<TResult>> mapValue, TResult none)
		=> _value is not null ? mapValue(_value) : Task.FromResult(none);

	/// <inheritdoc cref="M:Bogoware.Monads.Maybe`1.Match``1(System.Func{`0,``0},``0)"/>
	public Task<TResult> Match<TResult>(Func<TValue, Task<TResult>> mapValue, Func<TResult> none)
		=> _value is not null ? mapValue(_value) : Task.FromResult(none());

	/// <inheritdoc cref="M:Bogoware.Monads.Maybe`1.Match``1(System.Func{`0,``0},``0)"/>
	public Task<TResult> Match<TResult>(Func<TValue, Task<TResult>> mapValue, Func<Task<TResult>> none)
		=> _value is not null ? mapValue(_value) : none();

	/// <inheritdoc cref="M:Bogoware.Monads.Maybe`1.Match``1(System.Func{`0,``0},``0)"/>
	public Task<TResult> Match<TResult>(Func<TValue, TResult> mapValue, Func<Task<TResult>> none)
		=> _value is not null ? Task.FromResult(mapValue(_value)) : none();

	/// <summary>
	/// Execute the action if the <see cref="Maybe{T}"/> is <c>Some</c>.
	/// </summary>
	public Maybe<TValue> ExecuteIfSome(Action<TValue> action)
	{
		if (_value is not null) action(_value);
		return this;
	}

	/// <inheritdoc cref="ExecuteIfSome(System.Action{TValue})"/>
	public async Task<Maybe<TValue>> ExecuteIfSome(Func<TValue, Task> action)
	{
		if (_value is not null) await action(_value);
		return this;
	}

	/// <summary>
	/// Retrieve the value if present or return the <c>defaultValue</c> if missing.
	/// </summary>
	public TValue GetValue(TValue defaultValue)
	{
		ArgumentNullException.ThrowIfNull(defaultValue);
		return _value ?? defaultValue;
	}

	/// <inheritdoc cref="GetValue(TValue)"/>
	public TValue GetValue(Func<TValue> defaultValue)
	{
		ArgumentNullException.ThrowIfNull(defaultValue);
		return _value ?? defaultValue();
	}

	/// <inheritdoc cref="GetValue(TValue)"/>
	public async Task<TValue> GetValue(Func<Task<TValue>> defaultValue)
	{
		ArgumentNullException.ThrowIfNull(defaultValue);
		return _value ?? await defaultValue();
	}

	/// <summary>
	/// Downcast to <c>TNew</c> if possible, otherwise returns a <see cref="Maybe{TNew}"/>
	/// that is actually None case.
	/// </summary>
	/// <typeparam name="TNewValue"></typeparam>
	/// <returns></returns>
	public Maybe<TNewValue> OfType<TNewValue>() where TNewValue : class =>
		typeof(TValue).IsAssignableFrom(typeof(TNewValue))
			? new Maybe<TNewValue>(_value as TNewValue)
			: Maybe<TNewValue>.None;

	IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
	{
		if (_value is not null) yield return _value;
	}

	IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<TValue>)this).GetEnumerator();

	public override bool Equals(object? obj)
	{
		if (obj is Maybe<TValue> other) return Equals(other);
		return false;
	}

	public bool Equals(Maybe<TValue> other)
	{
		if (_value is not null) return _value?.Equals(other._value) ?? false;
		return other._value is null;
	}

	public override int GetHashCode() => _value?.GetHashCode() ?? 0;

	public static bool operator ==(Maybe<TValue> left, Maybe<TValue> right) => left.Equals(right);

	public static bool operator !=(Maybe<TValue> left, Maybe<TValue> right) => !left.Equals(right);

	public override string ToString() =>
		_value is null ? $"None<{typeof(TValue).GetFriendlyTypeName()}>()" : $"Some({_value})";

	public static implicit operator Maybe<TValue>(TValue? value) => new(value);
}