namespace Bogoware.Monads;

/// <summary>
/// Base exception for invalid operation performed against a <see cref="Result{TValue}"/>.
/// </summary>
public class ResultInvalidOperationException : InvalidOperationException
{
	protected ResultInvalidOperationException(string message)
		: base(message)
	{
	}
}