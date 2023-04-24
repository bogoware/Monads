namespace Bogoware.Monads;

/// <summary>
/// Base exception for invalid operation performed against a <see cref="Result{TValue,TError}"/>.
/// </summary>
public class ResultInvalidOperationException : InvalidOperationException
{
	protected ResultInvalidOperationException(string message)
		: base(message)
	{
	}
}