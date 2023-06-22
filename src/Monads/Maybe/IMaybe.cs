namespace Bogoware.Monads;

/// <summary>
/// Generic interface for <see cref="Maybe{TValue}"/>.
/// </summary>
public interface IMaybe
{
	bool IsSome { get; }
	bool IsNone { get; }
}

public interface IMaybe<in TValue> : IMaybe
{
}