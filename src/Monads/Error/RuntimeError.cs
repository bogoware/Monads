namespace Bogoware.Monads;

public class RuntimeError : Error
{
	public Exception Exception { get; }
	public RuntimeError(Exception exception)
	{
		ArgumentNullException.ThrowIfNull(exception);
		Exception = exception;
	}

	public override string Message => Exception.Message;
}