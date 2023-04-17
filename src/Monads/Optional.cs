using System.Diagnostics.CodeAnalysis;

// ReSharper disable ArrangeObjectCreationWhenTypeNotEvident

namespace Bogoware.Monads;

public readonly struct Optional<T> : IEquatable<Optional<T>>
	where T : class
{
	private readonly T? _value = default;
	public bool HasValue => _value is not null;
	public bool IsNone => _value is null;

	public Optional()
	{
	}

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
			: new Optional<TResult>();

	public Optional<TResult> Map<TResult>(Func<TResult> map) where TResult : class
		=> _value is not null
			? new Optional<TResult>(map())
			: new Optional<TResult>();

	public Optional<TResult> Map<TResult>(Func<T, TResult> map) where TResult : class
		=> _value is not null
			? new Optional<TResult>(map(_value))
			: new Optional<TResult>();

	public Optional<TResult> FlatMap<TResult>(Func<Optional<TResult>> map) where TResult : class 
		=> _value is not null
			? map()
			: new Optional<TResult>();

	public Optional<TResult> FlatMap<TResult>(Func<T, Optional<TResult>> map) where TResult : class 
		=> _value is not null
			? map(_value)
			: new Optional<TResult>();

	public T Default(T defaultValue) => _value ?? defaultValue;

	public T Default(Func<T> defaultValue) => _value ?? defaultValue();

	public Optional<TNew> OfType<TNew>() where TNew : class =>
		typeof(T).IsAssignableFrom(typeof(TNew))
			? new Optional<TNew>(_value as TNew)
			: new Optional<TNew>();

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

	public override string ToString() => _value is null ? $"None<{typeof(T).GetFriendlyTypeName()}>()" : $"Some({_value})";
}