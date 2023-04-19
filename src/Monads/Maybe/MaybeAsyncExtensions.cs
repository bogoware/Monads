using System.Runtime.CompilerServices;

namespace Bogoware.Monads;

public static class MaybeAsyncExtensions
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<TResult>> Map<T, TResult>(this Task<Maybe<T>> maybeTask, Func<TResult> map)
		where TResult : class
		where T : class
		=> (await maybeTask).Map(map);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<TResult>> Map<T, TResult>(
		this Task<Maybe<T>> maybeTask, Func<T, TResult> map)
		where TResult : class
		where T : class
		=> (await maybeTask).Map(map);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<TResult>> Map<T, TResult>(
		this Task<Maybe<T>> maybeTask, Func<Task<TResult>> map)
		where TResult : class
		where T : class
		=> await (await maybeTask).Map(map);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<TResult>> Map<T, TResult>(
		this Task<Maybe<T>> maybeTask, Func<T, Task<TResult>> map)
		where TResult : class
		where T : class
		=> await (await maybeTask).Map(map);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<T>> WithDefault<T>(
		this Task<Maybe<T>> maybeTask,
		T value) where T : class
		=> (await maybeTask).WithDefault(value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<T>> WithDefault<T>(
		this Task<Maybe<T>> maybeTask,
		Func<T> value) where T : class
		=> (await maybeTask).WithDefault(value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<T>> WithDefault<T>(
		this Task<Maybe<T>> maybeTask,
		Func<Task<T>> value) where T : class
		=> await (await maybeTask).WithDefault(value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<TResult>> Bind<T, TResult>(
		this Task<Maybe<T>> maybeTask, Func<Maybe<TResult>> map)
		where TResult : class
		where T : class
		=> (await maybeTask).Bind(map);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<TResult>> Bind<T, TResult>(
		this Task<Maybe<T>> maybeTask, Func<T, Maybe<TResult>> map)
		where TResult : class
		where T : class
		=> (await maybeTask).Bind(map);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<TResult>> Bind<T, TResult>(
		this Task<Maybe<T>> maybeTask, Func<Task<Maybe<TResult>>> map)
		where TResult : class
		where T : class
		=> await (await maybeTask).Bind(map);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<TResult>> Bind<T, TResult>(
		this Task<Maybe<T>> maybeTask, Func<T, Task<Maybe<TResult>>> map)
		where TResult : class
		where T : class
		=> await (await maybeTask).Bind(map);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<T, TResult>(
		this Task<Maybe<T>> maybeTask,
		TResult newValue,
		TResult none) where T : class
		=> (await maybeTask).Match(newValue, none);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<T, TResult>(
		this Task<Maybe<T>> maybeTask,
		Func<T, TResult> value,
		TResult none) where T : class
		=> (await maybeTask).Match(value, none);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<T, TResult>(
		this Task<Maybe<T>> maybeTask,
		Func<T, TResult> value,
		Func<TResult> none) where T : class
		=> (await maybeTask).Match(value, none);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<T, TResult>(
		this Task<Maybe<T>> maybeTask,
		Func<T, Task<TResult>> value,
		Func<TResult> none) where T : class
		=> await (await maybeTask).Match(value, none);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<T, TResult>(
		this Task<Maybe<T>> maybeTask,
		Func<T, TResult> value,
		Func<Task<TResult>> none) where T : class
		=> await (await maybeTask).Match(value, none);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<TResult> Match<T, TResult>(
		this Task<Maybe<T>> maybeTask,
		Func<T, Task<TResult>> value,
		Func<Task<TResult>> none) where T : class
		=> await (await maybeTask).Match(value, none);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<T>> IfSome<T>(
		this Task<Maybe<T>> maybeTask,
		Action action) where T : class
		=> (await maybeTask).IfSome(action);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<T>> IfSome<T>(
		this Task<Maybe<T>> maybeTask,
		Action<T> action) where T : class
		=> (await maybeTask).IfSome(action);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<T>> IfSome<T>(
		this Task<Maybe<T>> maybeTask,
		Func<Task> action) where T : class
		=> await (await maybeTask).IfSome(action);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<T>> IfSome<T>(
		this Task<Maybe<T>> maybeTask,
		Func<T, Task> action) where T : class
		=> await (await maybeTask).IfSome(action);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<T>> IfNone<T>(
		this Task<Maybe<T>> maybeTask,
		Action action) where T : class
		=> (await maybeTask).IfNone(action);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<T>> IfNone<T>(
		this Task<Maybe<T>> maybeTask,
		Func<Task> action) where T : class
		=> await (await maybeTask).IfNone(action);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<T>> Tap<T>(
		this Task<Maybe<T>> maybeTask,
		Action<Maybe<T>> action) where T : class
		=> (await maybeTask).Tap(action);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Maybe<T>> Tap<T>(
		this Task<Maybe<T>> maybeTask,
		Func<Maybe<T>, Task> action) where T : class
		=> await (await maybeTask).Tap(action);
}