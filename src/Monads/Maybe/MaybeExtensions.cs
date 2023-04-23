using System.Runtime.CompilerServices;

// ReSharper disable PossibleMultipleEnumeration

namespace Bogoware.Monads;

public static class MaybeExtensions
{
	/// <summary>
	/// Returns a <see cref="Maybe{T}"/> with <c>Some(first)</c> in case of non empty list. 
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Maybe<T> ToMaybe<T>(this IEnumerable<T>? values) where T : class
		=> values is not null && values.Any()
			? Prelude.Some(values.First())
			: Prelude.None<T>();

	/// <summary>
	/// Returns the original <c>Some</c> if predicate is satisfied, <c>None</c> otherwise.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Maybe<T> Where<T>(this T? obj, Func<T, bool> predicate) where T : class
		=> obj is not null && predicate(obj)
			? Prelude.Some(obj)
			: Prelude.None<T>();

	/// <summary>
	/// Returns the original <c>Some</c> if predicate is not satisfied, <c>None</c> otherwise.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Maybe<T> WhereNot<T>(this T? obj, Func<T, bool> predicate) where T : class
		=> obj is not null && !predicate(obj)
			? Prelude.Some(obj)
			: Prelude.None<T>();

	/// <summary>
	/// Evaluate the <see cref="predicate"/> applied to the value if present.
	/// Return <c>false</c> in case of <c>None</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool Satisfy<T>(this Maybe<T> maybe, Func<T, bool> predicate) where T : class
		=> maybe.Match(predicate, false);

	/// <inheritdoc cref="Satisfy{T}(Bogoware.Monads.Maybe{T},System.Func{T,bool})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<bool> Satisfy<T>(this Maybe<T> maybe, Func<T, Task<bool>> predicate) where T : class
		=> await maybe.Match(predicate, false);

	/// <summary>
	/// Execute the action.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Maybe<T> Execute<T>(this Maybe<T> maybe, Action<Maybe<T>> action) where T : class
	{
		action(maybe);
		return maybe;
	}

	/// <inheritdoc cref="Execute{T}(Bogoware.Monads.Maybe{T},System.Action{Bogoware.Monads.Maybe{T}})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<T>> Execute<T>(this Maybe<T> maybe, Func<Maybe<T>, Task> action) where T : class
	{
		await action(maybe);
		return maybe;
	}

	/// <summary>
	/// Execute the action if the <see cref="Maybe{T}"/> is <c>Some</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Maybe<T> ExecuteIfSome<T>(this Maybe<T> maybe, Action action) where T : class
	{
		if (maybe.IsSome) action();
		return maybe;
	}

	/// <inheritdoc cref="ExecuteIfSome{T}(Bogoware.Monads.Maybe{T},System.Action)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<T>> ExecuteIfSome<T>(this Maybe<T> maybe, Func<Task> action) where T : class
	{
		if (maybe.IsSome) await action();
		return maybe;
	}

	/// <summary>
	/// Execute the action if the <see cref="Maybe{T}"/> is <c>None</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Maybe<T> ExecuteIfNone<T>(this Maybe<T> maybe, Action action) where T : class
	{
		if (maybe.IsNone) action();
		return maybe;
	}

	/// <inheritdoc cref="ExecuteIfNone{T}(Bogoware.Monads.Maybe{T},System.Action)"/> 
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<T>> ExecuteIfNone<T>(this Maybe<T> maybe, Func<Task> action) where T : class
	{
		if (maybe.IsNone) await action();
		return maybe;
	}

	/// Map a default value if the current <see cref="Maybe{T}"/> is <c>None</c>. 
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Maybe<T> WithDefault<T>(this Maybe<T> maybe, T value) where T : class
		=> maybe.IsSome ? maybe : new(value);

	/// <inheritdoc cref="WithDefault{T}(Bogoware.Monads.Maybe{T},T)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Maybe<T> WithDefault<T>(this Maybe<T> maybe, Func<T> value) where T : class
		=> maybe.IsSome ? maybe : new(value());

	/// <inheritdoc cref="WithDefault{T}(Bogoware.Monads.Maybe{T},T)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<T>> WithDefault<T>(this Maybe<T> maybe, Func<Task<T>> value) where T : class
		=> maybe.IsSome ? maybe : new(await value());
}