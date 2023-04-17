namespace Bogoware.Monads;

public class OptionalValueMissingException : ArgumentNullException
{
	private const string ERROR_MESSAGE = "The value was <null>";
	public OptionalValueMissingException():base(ERROR_MESSAGE)
	{
	}
	public OptionalValueMissingException(Exception inner)
		: base(ERROR_MESSAGE, inner)
	{
	}
}