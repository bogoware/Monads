namespace Bogoware.Monads;

/// <summary>
/// Exception thrown when attempting to get the value
/// <see cref="Result{TValue,TError}"/> where <see cref="Result{TValue,TError}.IsFailure"/> is <c>true</c>.
/// </summary>
public class ResultFailedException : ResultInvalidOperationException
{
	private const string ERROR_MESSAGE = "Result is failed, consult Error for details.";
	/// <summary>
	/// The <see cref="Result{TValue,TError}"/> error.
	/// </summary>
	public Error Error { get; }
	public ResultFailedException(Error error):base(ERROR_MESSAGE)
	{
		Error = error;
	}
}