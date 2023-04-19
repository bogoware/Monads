namespace Bogoware.Monads;

public class ResultFailedException : InvalidOperationException
{
	private const string ERROR_MESSAGE = "Result is failed";
	public ResultFailedException():base(ERROR_MESSAGE)
	{
	}
	public ResultFailedException(Exception inner)
		: base(ERROR_MESSAGE, inner)
	{
	}
}