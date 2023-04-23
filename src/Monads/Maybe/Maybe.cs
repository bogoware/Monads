using System.Collections;

// ReSharper disable ArrangeObjectCreationWhenTypeNotEvident

namespace Bogoware.Monads;

/// <summary>
/// Represents an optional value.
/// </summary>
public readonly struct Maybe<T> : IMaybe, IEquatable<Maybe<T>>, IEnumerable<T>
	where T : class
{
	private readonly T? _value = default;
	public bool IsSome => _value is not null;
	public bool IsNone => _value is null;
	public static readonly Maybe<T> None = default;

	public Maybe(T? value)
	{
		if (value is not null)
		{
			_value = value;
		}
	}

	public Maybe(Maybe<T> maybe) => _value = maybe._value;

	/// <summary>
	/// Map the value to a new one.
	/// </summary>
	public Maybe<TResult> Map<TResult>(TResult value) where TResult : class
		=> IsSome
			? new Maybe<TResult>(value)
			: Maybe<TResult>.None;
	
	/// <inheritdoc cref="Map{TResult}(TResult)"/>
	public Maybe<TResult> Map<TResult>(Func<TResult> map) where TResult : class
		=> IsSome
			? new Maybe<TResult>(map())
			: Maybe<TResult>.None;
			

	/// <inheritdoc cref="Map{TResult}(TResult)"/>
	public Maybe<TResult> Map<TResult>(Func<T, TResult> map) where TResult : class
		=> _value is not null
			? new Maybe<TResult>(map(_value))
			: Maybe<TResult>.None;

	/// <inheritdoc cref="Map{TResult}(TResult)"/>
	public async Task<Maybe<TResult>> Map<TResult>(Func<Task<TResult>> map) where TResult : class
		=> IsSome
			? new(await map())
			: Maybe<TResult>.None;

	/// <inheritdoc cref="Map{TResult}(TResult)"/>
	public async Task<Maybe<TResult>> Map<TResult>(Func<T, Task<TResult>> map) where TResult : class
		=> _value is not null
			? new(await map(_value))
			: Maybe<TResult>.None;

	/// Bind a new <see cref="Maybe{TResult}"/>
	public Maybe<TResult> Bind<TResult>(Func<Maybe<TResult>> map) where TResult : class
		=> IsSome
			? map()
			: Maybe<TResult>.None;

	/// <inheritdoc cref="Bind{TResult}(System.Func{Bogoware.Monads.Maybe{TResult}})"/> 
	public Maybe<TResult> Bind<TResult>(Func<T, Maybe<TResult>> map) where TResult : class
		=> _value is not null
			? map(_value)
			: Maybe<TResult>.None;

	/// <inheritdoc cref="Bind{TResult}(System.Func{Bogoware.Monads.Maybe{TResult}})"/>
	public Task<Maybe<TResult>> Bind<TResult>(Func<Task<Maybe<TResult>>> map) where TResult : class
		=> IsSome
			? map()
			: Task.FromResult(Maybe<TResult>.None);

	/// <inheritdoc cref="Bind{TResult}(System.Func{Bogoware.Monads.Maybe{TResult}})"/>
	public Task<Maybe<TResult>> Bind<TResult>(Func<T, Task<Maybe<TResult>>> map) where TResult : class
		=> _value is not null
			? map(_value)
			: Task.FromResult(Maybe<TResult>.None);

	/// <summary>
	/// Maps a new value for both state of a <see cref="Maybe{T}"/> 
	/// </summary>
	public TResult Match<TResult>(TResult newValue, TResult none) 
		=> IsSome ? newValue : none;

	/// <inheritdoc cref="Match{TResult}(TResult,TResult)"/>
	public TResult Match<TResult>(Func<T, TResult> mapValue, TResult none)
		=> _value is not null
			? mapValue(_value)
			: none;

	/// <inheritdoc cref="Match{TResult}(TResult,TResult)"/>
	public TResult Match<TResult>(Func<T, TResult> mapValue, Func<TResult> none)
		=> _value is not null
			? mapValue(_value)
			: none();

	/// <inheritdoc cref="Match{TResult}(TResult,TResult)"/>
	public async Task<TResult> Match<TResult>(Func<T, Task<TResult>> mapValue, TResult none)
		=> _value is not null
			? await mapValue(_value)
			: none;

	/// <inheritdoc cref="Match{TResult}(TResult,TResult)"/>
	public async Task<TResult> Match<TResult>(Func<T, Task<TResult>> mapValue, Func<TResult> none)
		=> _value is not null
			? await mapValue(_value)
			: none();

	/// <inheritdoc cref="Match{TResult}(TResult,TResult)"/>
	public async Task<TResult> Match<TResult>(Func<T, Task<TResult>> mapValue, Func<Task<TResult>> none)
		=> _value is not null
			? await mapValue(_value)
			: await none();

	/// <inheritdoc cref="Match{TResult}(TResult,TResult)"/>
	public async Task<TResult> Match<TResult>(Func<T, TResult> mapValue, Func<Task<TResult>> none)
		=> _value is not null
			? mapValue(_value)
			: await none();


	/// <summary>
	/// Execute the action if the <see cref="Maybe{T}"/> is <c>Some</c>.
	/// </summary>
	public Maybe<T> ExecuteIfSome(Action<T> action)
	{
		if (_value is not null)
		{
			action(_value);
		}

		return this;
	}

	/// <inheritdoc cref="ExecuteIfSome(System.Action{T})"/>
	public async Task<Maybe<T>> ExecuteIfSome(Func<T, Task> action)
	{
		if (_value is not null)
		{
			await action(_value);
		}

		return this;
	}

	/// <summary>
	/// Retrieve the value if present or return the <see cref="defaultValue"/> if missing.
	/// </summary>
	public T GetValue(T defaultValue)
	{
		ArgumentNullException.ThrowIfNull(defaultValue);
		return _value ?? defaultValue;
	}

	/// <inheritdoc cref="GetValue(T)"/>
	public T GetValue(Func<T> defaultValue)
	{
		ArgumentNullException.ThrowIfNull(defaultValue);
		return _value ?? defaultValue();
	}

	/// <inheritdoc cref="GetValue(T)"/>
	public async Task<T> GetValue(Func<Task<T>> defaultValue)
	{
		ArgumentNullException.ThrowIfNull(defaultValue);
		return _value ?? await defaultValue();
	}

	/// <summary>
	/// Downcast to <see cref="TNew"/> if possible, otherwise returns a <see cref="Maybe{TNew}"/>
	/// that is actually None case.
	/// </summary>
	/// <typeparam name="TNew"></typeparam>
	/// <returns></returns>
	public Maybe<TNew> OfType<TNew>() where TNew : class =>
		typeof(T).IsAssignableFrom(typeof(TNew))
			? new Maybe<TNew>(_value as TNew)
			: Maybe<TNew>.None;

	IEnumerator<T> IEnumerable<T>.GetEnumerator()
	{
		if (_value is not null) yield return _value;
	}

	IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<T>)this).GetEnumerator();

	public override bool Equals(object? obj)
	{
		if (obj is null) return false;
		if (obj is Maybe<T> other) return Equals(other);
		return false;
	}

	public bool Equals(Maybe<T> other)
	{
		if (_value is not null) return _value?.Equals(other._value) ?? false;
		return other._value is null;
	}

	public override int GetHashCode() => _value?.GetHashCode() ?? 0;

	public static bool operator ==(Maybe<T> left, Maybe<T> right) => left.Equals(right);

	public static bool operator !=(Maybe<T> left, Maybe<T> right) => !left.Equals(right);

	public override string ToString() =>
		_value is null ? $"None<{typeof(T).GetFriendlyTypeName()}>()" : $"Some({_value})";

	public static implicit operator Maybe<T>(T? value) => new(value);
}