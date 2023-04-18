using System.Runtime.CompilerServices;

namespace Bogoware.Monads;

public static class OptionalAsyncExtensions
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Optional<TResult>> Map<T, TResult>(
		this Task<Optional<T>> optionalTask, Func<TResult> map)
		where TResult : class
		where T : class
		=> (await optionalTask).Map(map);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Optional<TResult>> Map<T, TResult>(
		this Task<Optional<T>> optionalTask, Func<T, TResult> map)
		where TResult : class
		where T : class
		=> (await optionalTask).Map(map);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Optional<TResult>> Map<T, TResult>(
		this Task<Optional<T>> optionalTask, Func<Task<TResult>> map)
		where TResult : class
		where T : class
		=> await (await optionalTask).Map(map);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Optional<TResult>> Map<T, TResult>(
		this Task<Optional<T>> optionalTask, Func<T, Task<TResult>> map)
		where TResult : class
		where T : class
		=> await (await optionalTask).Map(map);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Optional<TResult>> FlatMap<T, TResult>(
		this Task<Optional<T>> optionalTask, Func<Optional<TResult>> map)
		where TResult : class
		where T : class
		=> (await optionalTask).FlatMap(map);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Optional<TResult>> FlatMap<T, TResult>(
		this Task<Optional<T>> optionalTask, Func<T, Optional<TResult>> map)
		where TResult : class
		where T : class
		=> (await optionalTask).FlatMap(map);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Optional<TResult>> FlatMap<T, TResult>(
		this Task<Optional<T>> optionalTask, Func<Task<Optional<TResult>>> map)
		where TResult : class
		where T : class
		=> await (await optionalTask).FlatMap(map);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task<Optional<TResult>> FlatMap<T, TResult>(
		this Task<Optional<T>> optionalTask, Func<T, Task<Optional<TResult>>> map)
		where TResult : class
		where T : class
		=> await (await optionalTask).FlatMap(map);
}