namespace Bogoware.Monads;

public static class OptionalExtensions
{
	public static Optional<T> ToOptional<T>(this T? obj) where T : class => Prelude.Optional(obj);

	public static Optional<T> Where<T>(this T? obj, Func<T, bool> predicate) where T : class =>
		obj is not null && predicate(obj) ? Prelude.Some(obj) : Prelude.None<T>();

	public static Optional<T> WhereNot<T>(this T? obj, Func<T, bool> predicate) where T : class =>
		obj is not null && !predicate(obj) ? Prelude.Some(obj) : Prelude.None<T>();
}