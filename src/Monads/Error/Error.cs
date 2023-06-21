namespace Bogoware.Monads;

/// <summary>
/// Base class for all errors.
/// </summary>
public abstract class Error
{
	/// <summary>
	/// The error message.
	/// </summary>
	public abstract string Message { get; }
}