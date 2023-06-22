using Bogoware.Monads;

namespace Sample.CustomError;

public abstract class ApplicationError: LogicError
{
	
	public int ErrorCode { get; }

	protected ApplicationError(string message, int errorCode)
		: base(message)
	{
		ErrorCode = errorCode;
	}
}

public class NotFoundError : ApplicationError
{
	
	public string ResourceName { get; }
	public string ResourceId { get; }
	public NotFoundError(string message, int errorCode, string resourceName, string resourceId)
		: base(message, errorCode)
	{
		ResourceName = resourceName;
		ResourceId = resourceId;
	}
}

public class InvalidOperationError : ApplicationError
{
	
	public string OperationName { get; }
	public string Reason { get; }
	public InvalidOperationError(string message, int errorCode, string operationName, string reason)
		: base(message, errorCode)
	{
		OperationName = operationName;
		Reason = reason;
	}
}