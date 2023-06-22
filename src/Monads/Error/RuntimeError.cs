namespace Bogoware.Monads;

/// <summary>
/// Runtime errors are errors that depends on external factors
/// and are to be considered as exceptional.
/// For example: network errors, file system errors, etc.
/// </summary>
public sealed class RuntimeError : Error
{
	public Exception Exception { get; }
	public RuntimeError(Exception exception)
	{
		ArgumentNullException.ThrowIfNull(exception);
		Exception = exception;
	}

	public override string Message => Exception.Message;
}