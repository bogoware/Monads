namespace Bogoware.Monads;

/// <summary>
/// Exception thrown when attempting to get the error of a <see cref="Result{TValue}"/>
/// where <see cref="Result{TValue}.IsSuccess"/> is <c>true</c>.
/// </summary>
public class ResultSuccessException : ResultInvalidOperationException
{
	private const string ERROR_MESSAGE = "Result is successful";
	public ResultSuccessException():base(ERROR_MESSAGE)
	{
		
	}
}