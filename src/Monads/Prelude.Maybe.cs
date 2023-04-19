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
	public static Maybe<T> Some<T>(T value) where T : class
	{
		if (value is null) throw new MaybeValueMissingException();

		return new(value);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Maybe<T> None<T>() where T : class => Monads.Maybe<T>.None;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Maybe<Unit> None() => Monads.Maybe<Unit>.None;
}