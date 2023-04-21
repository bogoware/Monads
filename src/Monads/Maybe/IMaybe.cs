namespace Bogoware.Monads;

public interface IMaybe
{
	bool IsSome { get; }
	bool IsNone { get; }
}