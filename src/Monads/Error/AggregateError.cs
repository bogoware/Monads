// ReSharper disable MemberCanBePrivate.Global
namespace Bogoware.Monads;

/// <summary>
/// An error that aggregates multiple errors.
/// </summary>
public sealed class AggregateError : Error
{
	private const string ERROR_MESSAGE = "Multiple errors occurred";
	private readonly List<Error> _innerErrors;
	
	/// <summary>
	/// The errors that were aggregated.
	/// </summary>
	public IReadOnlyList<Error> Errors => _innerErrors;

	/// <summary>
	/// Initializes a new instance of the <see cref="AggregateError"/> class.
	/// </summary>
	/// <param name="message">The error message</param>
	/// <param name="innerErrors">The inner errors</param>
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

	private static IEnumerable<Error> ConcatenateErrors(Error first, Error second, IEnumerable<Error> others)
	{
		var errors = new List<Error> { first, second };
		return errors.Concat(others);
	}
}