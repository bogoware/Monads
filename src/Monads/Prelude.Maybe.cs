using System.Runtime.CompilerServices;
// ReSharper disable PossibleMultipleEnumeration

namespace Bogoware.Monads;

public static partial class Prelude
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Unit Unit() => Monads.Unit.Instance;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Maybe<T> Maybe<T>(T? value) where T : class => new(value);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Maybe<T> Maybe<T>(Maybe<T> maybe) where T : class => new(maybe);

	/// <summary>
	/// 
	/// </summary>
	/// <param name="value"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Maybe<T> Some<T>(T value) where T : class
	{
		ArgumentNullException.ThrowIfNull(value);

		return new(value);
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <param name="maybe"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	/// <exception cref="MaybeNoneException">If the maybe is <code>None</code></exception>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Maybe<T> Some<T>(Maybe<T> maybe) where T : class
	{
		if (maybe.IsNone) throw new MaybeNoneException();

		return new(maybe);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Maybe<T> None<T>() where T : class => Monads.Maybe<T>.None;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Maybe<Unit> None() => Monads.Maybe<Unit>.None;
}