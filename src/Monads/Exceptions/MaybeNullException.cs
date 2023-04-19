namespace Bogoware.Monads;

public class MaybeNullException : ArgumentNullException
{
	private const string ERROR_MESSAGE = "The value was <null>";
	public MaybeNullException():base(ERROR_MESSAGE)
	{
	}
	public MaybeNullException(Exception inner)
		: base(ERROR_MESSAGE, inner)
	{
	}
}