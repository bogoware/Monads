using Bogoware.Monads;

namespace FluentValidationSample;

public class ApplicationError: LogicError
{
	public ApplicationError(string message)
		: base(message)
	{
	}
}