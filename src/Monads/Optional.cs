using System.Collections;
using System.Runtime.CompilerServices;

// ReSharper disable ArrangeObjectCreationWhenTypeNotEvident

namespace Bogoware.Monads;

public readonly struct Optional<T> : IEquatable<Optional<T>>, IEnumerable<T>
	where T : class
{
	private readonly T? _value = default;
	public bool HasValue => _value is not null;
	public bool IsNone => _value is null;
	public static readonly Optional<T> None = default;

	public Optional(T? value)
	{
		if (value is not null)
		{
			_value = value;
		}
	}

	public Optional<TResult> Map<TResult>(TResult value) where TResult : class
		=> _value is not null
			? new Optional<TResult>(value)
			: Optional<TResult>.None;

	public Optional<TResult> Map<TResult>(Func<TResult> map) where TResult : class
		=> _value is not null
			? new Optional<TResult>(map())
			: Optional<TResult>.None;

	public Optional<TResult> Map<TResult>(Func<T, TResult> map) where TResult : class
		=> _value is not null
			? new Optional<TResult>(map(_value))
			: Optional<TResult>.None;

	public async Task<Optional<TResult>> Map<TResult>(Func<Task<TResult>> map) where TResult : class
		=> _value is not null
			? new(await map())
			: Optional<TResult>.None;

	public async Task<Optional<TResult>> Map<TResult>(Func<T, Task<TResult>> map) where TResult : class
		=> _value is not null
			? new(await map(_value))
			: Optional<TResult>.None;

	public Optional<TResult> Bind<TResult>(Func<Optional<TResult>> map) where TResult : class
		=> _value is not null
			? map()
			: Optional<TResult>.None;

	public Optional<TResult> Bind<TResult>(Func<T, Optional<TResult>> map) where TResult : class
		=> _value is not null
			? map(_value)
			: Optional<TResult>.None;

	public Task<Optional<TResult>> Bind<TResult>(Func<Task<Optional<TResult>>> map) where TResult : class
		=> _value is not null
			? map()
			: Task.FromResult(Optional<TResult>.None);

	public Task<Optional<TResult>> Bind<TResult>(Func<T, Task<Optional<TResult>>> map) where TResult : class
		=> _value is not null
			? map(_value)
			: Task.FromResult(Optional<TResult>.None);

	public Optional<T> WithDefault(T value)
		=> _value is not null
			? this
			: new(value);
	
	public Optional<T> WithDefault(Func<T> value)
		=> _value is not null
			? this
			: new(value());
	
	public async Task<Optional<T>> WithDefault(Func<Task<T>> value)
		=> _value is not null
			? this
			: new(await value());

	public Optional<TResult> Match<TResult>(TResult newValue, TResult none) where TResult : class
		=> _value is not null 
			? newValue 
			: none;

	public Optional<TResult> Match<TResult>(Func<T, TResult> mapValue, TResult none) where TResult : class
		=> _value is not null 
			? mapValue(_value) 
			: none;
	
	public Optional<TResult> Match<TResult>(Func<T, TResult> mapValue, Func<TResult> none) where TResult : class
		=> _value is not null 
			? mapValue(_value) 
			: none();
	
	public async Task<Optional<TResult>> Match<TResult>(Func<T, Task<TResult>> mapValue, Func<TResult> none) where TResult : class
		=> _value is not null 
			? await mapValue(_value) 
			: none();
	
	public async Task<Optional<TResult>> Match<TResult>(Func<T, Task<TResult>> mapValue, Func<Task<TResult>> none) where TResult : class
		=> _value is not null 
			? await mapValue(_value) 
			: await none();
	
	public async Task<Optional<TResult>> Match<TResult>(Func<T, TResult> mapValue, Func<Task<TResult>> none) where TResult : class
		=> _value is not null 
			? mapValue(_value) 
			: await none();

	public T GetValue(T defaultValue)
	{
		ArgumentNullException.ThrowIfNull(defaultValue);
		return _value ?? defaultValue;
	}

	public T GetValue(Func<T> defaultValue)
	{
		ArgumentNullException.ThrowIfNull(defaultValue);
		return _value ?? defaultValue();
	}

	public Optional<TNew> OfType<TNew>() where TNew : class =>
		typeof(T).IsAssignableFrom(typeof(TNew))
			? new Optional<TNew>(_value as TNew)
			: Optional<TNew>.None;

	IEnumerator<T> IEnumerable<T>.GetEnumerator()
	{
		if (HasValue) yield return _value!;
	}

	IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<T>)this).GetEnumerator();

	public override bool Equals(object? obj)
	{
		if (obj is null) return false;
		if (obj is Optional<T> other) return Equals(other);
		return false;
	}

	public bool Equals(Optional<T> other)
	{
		if (_value is not null) return _value?.Equals(other._value) ?? false;
		return other._value is null;
	}

	public override int GetHashCode() => _value?.GetHashCode() ?? 0;

	public static bool operator ==(Optional<T> left, Optional<T> right) => left.Equals(right);

	public static bool operator !=(Optional<T> left, Optional<T> right) => !left.Equals(right);

	public override string ToString() =>
		_value is null ? $"None<{typeof(T).GetFriendlyTypeName()}>()" : $"Some({_value})";

	public static implicit operator Optional<T>(T? value) => new(value);
}