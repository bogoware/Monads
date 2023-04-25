// ReSharper disable UnusedMember.Global
namespace Bogoware.Monads;

public class MaybeNoneException : ArgumentNullException
{
	private const string ERROR_MESSAGE = "The Maybe is None";
	public MaybeNoneException():base(ERROR_MESSAGE)
	{
	}
	public MaybeNoneException(Exception inner)
		: base(ERROR_MESSAGE, inner)
	{
	}
}