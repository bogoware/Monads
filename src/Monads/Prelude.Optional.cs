using System.Collections;
using System.Runtime.CompilerServices;
// ReSharper disable PossibleMultipleEnumeration

namespace Bogoware.Monads;

public static partial class Prelude
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Unit Unit() => Monads.Unit.Instance;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Optional<T> Optional<T>(T? value) where T : class => new(value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Optional<T> Some<T>(T value) where T : class
	{
		if (value is null) throw new OptionalValueMissingException();

		return new(value);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Optional<T> None<T>() where T : class => new();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Optional<Unit> None() => new();
}