namespace Bogoware.Monads;

/// <summary>
/// Exception thrown when attempting to get the value
/// <see cref="Result{TValue,TError}"/> where <see cref="Result{TValue,TError}.IsSuccess"/> is <c>true</c>.
/// </summary>
public class ResultSuccessException : ResultInvalidOperationException
{
	private const string ERROR_MESSAGE = "Result is successful";
	public ResultSuccessException():base(ERROR_MESSAGE)
	{
		
	}
}