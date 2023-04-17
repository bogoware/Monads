namespace Bogoware.Monads;

public static class Prelude
{
	public static Unit Unit() => Monads.Unit.Instance;

	public static Optional<T> Optional<T>(T? value) where T : class => new(value);
	public static Optional<T> Some<T>(T value) where T : class
	{
		if (value is null)
		{
			throw new OptionalValueMissingException();
		}
		return new(value);
	}
	public static Optional<T> None<T>() where T : class => new();
	public static Optional<Unit> None() => new();
}