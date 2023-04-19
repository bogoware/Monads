namespace Bogoware.Monads;

public class MaybeValueMissingException : ArgumentNullException
{
	private const string ERROR_MESSAGE = "The value was <null>";
	public MaybeValueMissingException():base(ERROR_MESSAGE)
	{
	}
	public MaybeValueMissingException(Exception inner)
		: base(ERROR_MESSAGE, inner)
	{
	}
}