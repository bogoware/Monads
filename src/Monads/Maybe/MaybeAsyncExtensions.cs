using System.Runtime.CompilerServices;
// ReSharper disable UnusedMethodReturnValue.Global

namespace Bogoware.Monads;

public static class MaybeAsyncExtensions
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<TNewValue>> Map<TValue, TNewValue>(this Task<Maybe<TValue>> maybeTask, Func<TNewValue> map)
		where TNewValue : class
		where TValue : class
		=> (await maybeTask).Map(map);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<TNewValue>> Map<TValue, TNewValue>(
		this Task<Maybe<TValue>> maybeTask, Func<TValue, TNewValue> map)
		where TNewValue : class
		where TValue : class
		=> (await maybeTask).Map(map);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<TNewValue>> Map<TValue, TNewValue>(
		this Task<Maybe<TValue>> maybeTask, Func<Task<TNewValue>> map)
		where TNewValue : class
		where TValue : class
		=> await (await maybeTask).Map(map);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<TNewValue>> Map<TValue, TNewValue>(
		this Task<Maybe<TValue>> maybeTask, Func<TValue, Task<TNewValue>> map)
		where TNewValue : class
		where TValue : class
		=> await (await maybeTask).Map(map);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<TValue>> WithDefault<TValue>(
		this Task<Maybe<TValue>> maybeTask,
		TValue value) where TValue : class
		=> (await maybeTask).WithDefault(value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<TValue>> WithDefault<TValue>(
		this Task<Maybe<TValue>> maybeTask,
		Func<TValue> value) where TValue : class
		=> (await maybeTask).WithDefault(value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<TValue>> WithDefault<TValue>(
		this Task<Maybe<TValue>> maybeTask,
		Func<Task<TValue>> value) where TValue : class
		=> await (await maybeTask).WithDefault(value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<TNewValue>> Bind<TValue, TNewValue>(
		this Task<Maybe<TValue>> maybeTask, Func<Maybe<TNewValue>> map)
		where TNewValue : class
		where TValue : class
		=> (await maybeTask).Bind(map);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<TNewValue>> Bind<TValue, TNewValue>(
		this Task<Maybe<TValue>> maybeTask, Func<TValue, Maybe<TNewValue>> map)
		where TNewValue : class
		where TValue : class
		=> (await maybeTask).Bind(map);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<TNewValue>> Bind<TValue, TNewValue>(
		this Task<Maybe<TValue>> maybeTask, Func<Task<Maybe<TNewValue>>> map)
		where TNewValue : class
		where TValue : class
		=> await (await maybeTask).Bind(map);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<TNewValue>> Bind<TValue, TNewValue>(
		this Task<Maybe<TValue>> maybeTask, Func<TValue, Task<Maybe<TNewValue>>> map)
		where TNewValue : class
		where TValue : class
		=> await (await maybeTask).Bind(map);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<TValue, TResult>(
		this Task<Maybe<TValue>> maybeTask,
		TResult newValue,
		TResult none) where TValue : class
		=> (await maybeTask).Match(newValue, none);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<TValue, TResult>(
		this Task<Maybe<TValue>> maybeTask,
		Func<TValue, TResult> value,
		TResult none) where TValue : class
		=> (await maybeTask).Match(value, none);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<TValue, TResult>(
		this Task<Maybe<TValue>> maybeTask,
		Func<TValue, TResult> value,
		Func<TResult> none) where TValue : class
		=> (await maybeTask).Match(value, none);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<TValue, TResult>(
		this Task<Maybe<TValue>> maybeTask,
		Func<TValue, Task<TResult>> value,
		Func<TResult> none) where TValue : class
		=> await (await maybeTask).Match(value, none);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<TValue, TResult>(
		this Task<Maybe<TValue>> maybeTask,
		Func<TValue, TResult> value,
		Func<Task<TResult>> none) where TValue : class
		=> await (await maybeTask).Match(value, none);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<TValue, TResult>(
		this Task<Maybe<TValue>> maybeTask,
		Func<TValue, Task<TResult>> mapValue, TResult none) where TValue : class
		=> await (await maybeTask).Match(mapValue, none);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<TValue, TResult>(
		this Task<Maybe<TValue>> maybeTask,
		Func<TValue, Task<TResult>> value,
		Func<Task<TResult>> none) where TValue : class
		=> await (await maybeTask).Match(value, none);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<TValue>> ExecuteIfSome<TValue>(
		this Task<Maybe<TValue>> maybeTask,
		Action action) where TValue : class
		=> (await maybeTask).ExecuteIfSome(action);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<TValue>> ExecuteIfSome<TValue>(
		this Task<Maybe<TValue>> maybeTask,
		Action<TValue> action) where TValue : class
		=> (await maybeTask).ExecuteIfSome(action);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<TValue>> ExecuteIfSome<TValue>(
		this Task<Maybe<TValue>> maybeTask,
		Func<Task> action) where TValue : class
		=> await (await maybeTask).ExecuteIfSome(action);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<TValue>> ExecuteIfSome<TValue>(
		this Task<Maybe<TValue>> maybeTask,
		Func<TValue, Task> action) where TValue : class
		=> await (await maybeTask).ExecuteIfSome(action);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<TValue>> ExecuteIfNone<TValue>(
		this Task<Maybe<TValue>> maybeTask,
		Action action) where TValue : class
		=> (await maybeTask).ExecuteIfNone(action);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<TValue>> ExecuteIfNone<TValue>(
		this Task<Maybe<TValue>> maybeTask,
		Func<Task> action) where TValue : class
		=> await (await maybeTask).ExecuteIfNone(action);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<TValue>> Execute<TValue>(
		this Task<Maybe<TValue>> maybeTask,
		Action<Maybe<TValue>> action) where TValue : class
		=> (await maybeTask).Execute(action);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<TValue>> Execute<TValue>(
		this Task<Maybe<TValue>> maybeTask,
		Func<Maybe<TValue>, Task> action) where TValue : class
		=> await (await maybeTask).Execute(action);
	
	
	
	/// <summary>
	/// Evaluate the <c>predicate</c> applied to the value if present.
	/// Return <c>false</c> in case of <c>None</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Task<bool> Satisfy<TValue>(
		this Task<Maybe<TValue>> maybe, Func<TValue, bool> predicate) where TValue : class
		=> maybe.Match(predicate, false);

	/// <inheritdoc cref="Satisfy{TValue}(Bogoware.Monads.Maybe{TValue},System.Func{TValue,System.Threading.Tasks.Task{bool}})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Task<bool> Satisfy<TValue>(
		this Task<Maybe<TValue>> maybe, Func<TValue, Task<bool>> predicate) where TValue : class
		=> maybe.Match(predicate, false);
}