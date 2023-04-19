using System.Collections;

// ReSharper disable ArrangeObjectCreationWhenTypeNotEvident

namespace Bogoware.Monads;

public readonly struct Maybe<T> : IEquatable<Maybe<T>>, IEnumerable<T>
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

	public Maybe<TResult> Map<TResult>(TResult value) where TResult : class
		=> _value is not null
			? new Maybe<TResult>(value)
			: Maybe<TResult>.None;

	public Maybe<TResult> Map<TResult>(Func<TResult> map) where TResult : class
		=> _value is not null
			? new Maybe<TResult>(map())
			: Maybe<TResult>.None;

	public Maybe<TResult> Map<TResult>(Func<T, TResult> map) where TResult : class
		=> _value is not null
			? new Maybe<TResult>(map(_value))
			: Maybe<TResult>.None;

	public async Task<Maybe<TResult>> Map<TResult>(Func<Task<TResult>> map) where TResult : class
		=> _value is not null
			? new(await map())
			: Maybe<TResult>.None;

	public async Task<Maybe<TResult>> Map<TResult>(Func<T, Task<TResult>> map) where TResult : class
		=> _value is not null
			? new(await map(_value))
			: Maybe<TResult>.None;

	public Maybe<TResult> Bind<TResult>(Func<Maybe<TResult>> map) where TResult : class
		=> _value is not null
			? map()
			: Maybe<TResult>.None;

	public Maybe<TResult> Bind<TResult>(Func<T, Maybe<TResult>> map) where TResult : class
		=> _value is not null
			? map(_value)
			: Maybe<TResult>.None;

	public Task<Maybe<TResult>> Bind<TResult>(Func<Task<Maybe<TResult>>> map) where TResult : class
		=> _value is not null
			? map()
			: Task.FromResult(Maybe<TResult>.None);

	public Task<Maybe<TResult>> Bind<TResult>(Func<T, Task<Maybe<TResult>>> map) where TResult : class
		=> _value is not null
			? map(_value)
			: Task.FromResult(Maybe<TResult>.None);

	public Maybe<T> WithDefault(T value)
		=> _value is not null
			? this
			: new(value);
	
	public Maybe<T> WithDefault(Func<T> value)
		=> _value is not null
			? this
			: new(value());
	
	public async Task<Maybe<T>> WithDefault(Func<Task<T>> value)
		=> _value is not null
			? this
			: new(await value());

	public TResult Match<TResult>(TResult newValue, TResult none)
		=> _value is not null 
			? newValue 
			: none;

	public TResult Match<TResult>(Func<T, TResult> mapValue, TResult none)
		=> _value is not null 
			? mapValue(_value) 
			: none;
	
	public TResult Match<TResult>(Func<T, TResult> mapValue, Func<TResult> none)
		=> _value is not null 
			? mapValue(_value) 
			: none();
	
	public async Task<TResult> Match<TResult>(Func<T, Task<TResult>> mapValue, Func<TResult> none)
		=> _value is not null 
			? await mapValue(_value) 
			: none();
	
	public async Task<TResult> Match<TResult>(Func<T, Task<TResult>> mapValue, Func<Task<TResult>> none)
		=> _value is not null 
			? await mapValue(_value) 
			: await none();
	
	public async Task<TResult> Match<TResult>(Func<T, TResult> mapValue, Func<Task<TResult>> none)
		=> _value is not null 
			? mapValue(_value) 
			: await none();

	public Maybe<T> IfSome(Action action)
	{
		if (_value is not null)
		{
			action();
		}

		return this;
	}
	public Maybe<T> IfSome(Action<T> action)
	{
		if (_value is not null)
		{
			action(_value);
		}

		return this;
	}
	
	public async Task<Maybe<T>> IfSome(Func<Task> action)
	{
		if (_value is not null)
		{
			await action();
		}

		return this;
	}

	public async Task<Maybe<T>> IfSome(Func<T, Task> action)
	{
		if (_value is not null)
		{
			await action(_value);
		}

		return this;
	}
	
	public Maybe<T> IfNone(Action action)
	{
		if (_value is null)
		{
			action();
		}

		return this;
	}
	
	public async Task<Maybe<T>> IfNone(Func<Task> action)
	{
		if (_value is null)
		{
			await action();
		}

		return this;
	}

	public Maybe<T> Tap(Action<Maybe<T>> action)
	{
		action(this);
		return this;
	}

	public async Task<Maybe<T>> Tap(Func<Maybe<T>, Task> action)
	{
		await action(this);
		return this;
	}

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

	public Maybe<TNew> OfType<TNew>() where TNew : class =>
		typeof(T).IsAssignableFrom(typeof(TNew))
			? new Maybe<TNew>(_value as TNew)
			: Maybe<TNew>.None;

	IEnumerator<T> IEnumerable<T>.GetEnumerator()
	{
		if (IsSome) yield return _value!;
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