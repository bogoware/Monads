using System.Runtime.CompilerServices;
// ReSharper disable UnusedMember.Global

// ReSharper disable PossibleMultipleEnumeration

namespace Bogoware.Monads;

public static class MaybeExtensions
{
	/// <summary>
	/// Map the value to a new one.
	/// </summary>
	public static Maybe<TNewValue> Map<TValue, TNewValue>(this Maybe<TValue> maybe, TNewValue? value)
		where TNewValue : class where TValue : class
		=> maybe.IsSome ? new(value) : Maybe<TNewValue>.None;

	/// <inheritdoc cref="Map{TValue,TResult}(Bogoware.Monads.Maybe{TValue},TResult)"/>
	public static Maybe<TNewValue> Map<TValue, TNewValue>(this Maybe<TValue> maybe, Func<TNewValue?> map)
		where TNewValue : class where TValue : class
		=> maybe.IsSome ? new(map()) : Maybe<TNewValue>.None;

	/// <inheritdoc cref="Map{TValue,TResult}(Bogoware.Monads.Maybe{TValue},TResult)"/>
	public static async Task<Maybe<TNewValue>> Map<TValue, TNewValue>(this Maybe<TValue> maybe, Func<Task<TNewValue?>> map)
		where TNewValue : class where TValue : class
		=> maybe.IsSome ? new(await map()) : Maybe<TNewValue>.None;

	/// <summary>
	/// Bind the maybe and, possibly, to a new one.
	/// </summary>
	public static Maybe<TNewValue> Bind<TValue, TNewValue>(this Maybe<TValue> maybe, Func<Maybe<TNewValue>> map)
		where TNewValue : class where TValue : class
		=> maybe.IsSome ? map() : Maybe<TNewValue>.None;

	/// <inheritdoc cref="Bind{TValue,TResult}(Bogoware.Monads.Maybe{TValue},System.Func{Bogoware.Monads.Maybe{TResult}})"/>
	public static Task<Maybe<TNewValue>> Bind<TValue, TNewValue>(this Maybe<TValue> maybe, Func<Task<Maybe<TNewValue>>> map)
		where TNewValue : class where TValue : class
		=> maybe.IsSome ? map() : Task.FromResult(Maybe<TNewValue>.None);

	/// <summary>
	/// Maps a new value for both state of a <see cref="Maybe{T}"/> 
	/// </summary>
	public static TResult Match<TValue, TResult>(this Maybe<TValue> maybe, TResult resultOnValue, TResult resultOnNone)
		where TValue : class
		=> maybe.IsSome ? resultOnValue : resultOnNone;

	/// <inheritdoc cref="Match{TValue,TResult}(Bogoware.Monads.Maybe{TValue},TResult,TResult)"/>
	public static TResult Match<TValue, TResult>(this Maybe<TValue> maybe, Func<TResult> resultOnValue, TResult resultOnNone)
		where TValue : class
		=> maybe.IsSome ? resultOnValue() : resultOnNone;

	/// <inheritdoc cref="Match{TValue,TResult}(Bogoware.Monads.Maybe{TValue},TResult,TResult)"/>
	public static TResult Match<TValue, TResult>(this Maybe<TValue> maybe, TResult resultOnValue, Func<TResult> resultOnNone)
		where TValue : class
		=> maybe.IsSome ? resultOnValue : resultOnNone();

	/// <inheritdoc cref="Match{TValue,TResult}(Bogoware.Monads.Maybe{TValue},TResult,TResult)"/>
	public static TResult Match<TValue, TResult>(this Maybe<TValue> maybe, Func<TResult> resultOnValue, Func<TResult> resultOnNone)
		where TValue : class
		=> maybe.IsSome ? resultOnValue() : resultOnNone();
	
	/// <inheritdoc cref="Match{TValue,TResult}(Bogoware.Monads.Maybe{TValue},TResult,TResult)"/>
	public static Task<TResult> Match<TValue, TResult>(this Maybe<TValue> maybe, Func<Task<TResult>> resultOnValue, TResult resultOnNone)
		where TValue : class
		=> maybe.IsSome ? resultOnValue() : Task.FromResult(resultOnNone);

	/// <inheritdoc cref="Match{TValue,TResult}(Bogoware.Monads.Maybe{TValue},TResult,TResult)"/>
	public static Task<TResult> Match<TValue, TResult>(this Maybe<TValue> maybe, TResult resultOnValue, Func<Task<TResult>> resultOnNone)
		where TValue : class
		=> maybe.IsSome ? Task.FromResult(resultOnValue) : resultOnNone();

	/// <inheritdoc cref="Match{TValue,TResult}(Bogoware.Monads.Maybe{TValue},TResult,TResult)"/>
	public static Task<TResult> Match<TValue, TResult>(this Maybe<TValue> maybe, Func<Task<TResult>> resultOnValue, Func<Task<TResult>> resultOnNone)
		where TValue : class
		=> maybe.IsSome ? resultOnValue() : resultOnNone();

	/// <summary>
	/// Returns a <see cref="Maybe{T}"/> with <c>Some(first)</c> in case of non empty list. 
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Maybe<TValue> ToMaybe<TValue>(this IEnumerable<TValue>? values) where TValue : class
		=> values is not null && values.Any()
			? Maybe.Some(values.First())
			: Maybe.None<TValue>();

	/// <summary>
	/// Returns the original <c>Some</c> if predicate is satisfied, <c>None</c> otherwise.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Maybe<TValue> Where<TValue>(this TValue? obj, Func<TValue, bool> predicate) where TValue : class
		=> obj is not null && predicate(obj)
			? Maybe.Some(obj)
			: Maybe.None<TValue>();

	/// <summary>
	/// Returns the original <c>Some</c> if predicate is not satisfied, <c>None</c> otherwise.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Maybe<TValue> WhereNot<TValue>(this TValue? obj, Func<TValue, bool> predicate) where TValue : class
		=> obj is not null && !predicate(obj)
			? Maybe.Some(obj)
			: Maybe.None<TValue>();

	/// <summary>
	/// Evaluate the <c>predicate</c> applied to the value if present.
	/// Return <c>false</c> in case of <c>None</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool Satisfy<TValue>(this Maybe<TValue> maybe, Func<TValue, bool> predicate) where TValue : class
		=> maybe.Match(predicate, false);

	/// <inheritdoc cref="Satisfy{TValue}(Bogoware.Monads.Maybe{TValue},System.Func{TValue,System.Threading.Tasks.Task{bool}})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<bool> Satisfy<TValue>(this Maybe<TValue> maybe, Func<TValue, Task<bool>> predicate) where TValue : class
		=> await maybe.Match(predicate, false);

	/// <summary>
	/// Execute the action.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ref readonly Maybe<TValue> Execute<TValue>(in this Maybe<TValue> maybe, Action<Maybe<TValue>> action) where TValue : class
	{
		action(maybe);
		return ref maybe;
	}

	/// <inheritdoc cref="T:Bogoware.Monads.Maybe`1"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<TValue>> Execute<TValue>(this Maybe<TValue> maybe, Func<Maybe<TValue>, Task> action) where TValue : class
	{
		await action(maybe);
		return maybe;
	}

	/// <summary>
	/// Execute the action if the <see cref="Maybe{T}"/> is <c>Some</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ref readonly Maybe<TValue> ExecuteIfSome<TValue>(in this Maybe<TValue> maybe, Action action) where TValue : class
	{
		if (maybe.IsSome) action();
		return ref maybe;
	}

	/// <inheritdoc cref="M:Bogoware.Monads.MaybeExtensions.ExecuteIfSome``1(Bogoware.Monads.Maybe{``0}@,System.Action)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<TNewValue>> ExecuteIfSome<TNewValue>(this Maybe<TNewValue> maybe, Func<Task> action) where TNewValue : class
	{
		if (maybe.IsSome) await action();
		return maybe;
	}

	/// <summary>
	/// Execute the action if the <see cref="Maybe{T}"/> is <c>None</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ref readonly Maybe<TNewValue> ExecuteIfNone<TNewValue>(in this Maybe<TNewValue> maybe, Action action) where TNewValue : class
	{
		if (maybe.IsNone) action();
		return ref maybe;
	}

	/// <inheritdoc cref="M:Bogoware.Monads.MaybeExtensions.ExecuteIfNone``1(Bogoware.Monads.Maybe{``0}@,System.Action)"/> 
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<TNewValue>> ExecuteIfNone<TNewValue>(this Maybe<TNewValue> maybe, Func<Task> action) where TNewValue : class
	{
		if (maybe.IsNone) await action();
		return maybe;
	}

	/// Map a default value if the current <see cref="Maybe{T}"/> is <c>None</c>. 
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Maybe<TNewValue> WithDefault<TNewValue>(this Maybe<TNewValue> maybe, TNewValue value) where TNewValue : class
		=> maybe.IsSome ? maybe : new(value);

	/// <inheritdoc cref="WithDefault{T}(Bogoware.Monads.Maybe{T},T)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Maybe<TNewValue> WithDefault<TNewValue>(this Maybe<TNewValue> maybe, Func<TNewValue> value) where TNewValue : class
		=> maybe.IsSome ? maybe : new(value());

	/// <inheritdoc cref="WithDefault{T}(Bogoware.Monads.Maybe{T},T)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<TNewValue>> WithDefault<TNewValue>(this Maybe<TNewValue> maybe, Func<Task<TNewValue>> value) where TNewValue : class
		=> maybe.IsSome ? maybe : new(await value());
}