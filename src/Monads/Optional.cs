using System.Collections;

// ReSharper disable ArrangeObjectCreationWhenTypeNotEvident

namespace Bogoware.Monads;

public readonly struct Optional<T> : IEquatable<Optional<T>>, IEnumerable<T>
	where T : class
{
	#region Enumerators
	private class SomeEnumerator : IEnumerator<T>
	{
		private readonly T _value;
		private T? _current;

		public SomeEnumerator(T value)
		{
			_value = value;
			_current = null!;
		}

		public bool MoveNext()
		{
			if (_current is null)
			{
				_current = _value;
				return true;
			}

			return false;
		}

		public void Reset() => _current = null;

		public T Current
		{
			get
			{
				if (_current is null)
					throw new InvalidOperationException("Some value already enumerated");
				return _current;
			}
		}

		object IEnumerator.Current => Current;

		public void Dispose() => Reset();
	}

	private class NoneEnumerator : IEnumerator<T>
	{
		public bool MoveNext() => false;

		public void Reset()
		{
		}

		public T Current => throw new InvalidOperationException("None cannot be enumerated");

		object IEnumerator.Current => Current;

		public void Dispose()
		{
		}
	}

	private readonly NoneEnumerator _noneEnumeratorInstance = new();
	#endregion Enumerators

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
	
	public async Task<Optional<TResult>> Map<TResult>(Func<Task<TResult>> map) where TResult : class
		=> _value is not null
			? new(await map())
			: new();
	
	public async Task<Optional<TResult>> Map<TResult>(Func<T, Task<TResult>> map) where TResult : class
		=> _value is not null
			? new(await map(_value))
			: new();

	public Optional<TResult> FlatMap<TResult>(Func<Optional<TResult>> map) where TResult : class
		=> _value is not null
			? map()
			: new Optional<TResult>();

	public Optional<TResult> FlatMap<TResult>(Func<T, Optional<TResult>> map) where TResult : class
		=> _value is not null
			? map(_value)
			: new Optional<TResult>();
	
	public Task<Optional<TResult>> FlatMap<TResult>(Func<Task<Optional<TResult>>> map) where TResult : class
		=> _value is not null
			? map()
			: Task.FromResult(new Optional<TResult>());

	public Task<Optional<TResult>> FlatMap<TResult>(Func<T, Task<Optional<TResult>>> map) where TResult : class
		=> _value is not null
			? map(_value)
			: Task.FromResult(new Optional<TResult>());

	public T Default(T defaultValue) => _value ?? defaultValue;

	public T Default(Func<T> defaultValue) => _value ?? defaultValue();

	public Optional<TNew> OfType<TNew>() where TNew : class =>
		typeof(T).IsAssignableFrom(typeof(TNew))
			? new Optional<TNew>(_value as TNew)
			: new Optional<TNew>();

	IEnumerator<T> IEnumerable<T>.GetEnumerator()
	{
		if (IsNone) return _noneEnumeratorInstance;
		return new SomeEnumerator(_value!);
	}

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

	IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<T>)this).GetEnumerator();
	
	public static implicit operator Optional<T> (T? value) => new(value);
}