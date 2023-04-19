using System.Net.Security;

namespace Bogoware.Monads;

public sealed class AggregateError : Error
{
	private const string ERROR_MESSAGE = "Multiple errors occurred";
	private readonly List<Error> _innerErrors;
	public IReadOnlyList<Error> Errors => _innerErrors;

	public AggregateError(string message, IEnumerable<Error> innerErrors)
	{
		ArgumentNullException.ThrowIfNull(message);
		ArgumentNullException.ThrowIfNull(innerErrors);
		Message = message;
		_innerErrors = innerErrors.ToList();
	}

	public AggregateError(string message, Error first, Error second, params Error[] others)
		: this(message, ConcatenateErrors(first, second, others))
	{
	}

	public AggregateError(IEnumerable<Error> innerErrors)
		: this(ERROR_MESSAGE, innerErrors)
	{
	}
	
	public AggregateError(Error first, Error second, params Error[] others)
		: this(ConcatenateErrors(first, second, others))
	{
	}

	public override string Message { get; }

	private static IEnumerable<Error> ConcatenateErrors(Error first, Error second, Error[] others)
	{
		var errors = new List<Error>() { first, second };
		return errors.Concat(others);
	}
}