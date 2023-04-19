namespace Bogoware.Monads;


public readonly struct Result<TValue, TError>
	where TError: Error
{
	private readonly TValue? _value;
	private readonly TError? _error;

	public Result(TValue value) => _value = value;
	public Result(TError error) => _error = error;

	public bool IsSuccess => _value is not null;
	public bool IsFailure => _error is not null;

	/// <summary>
	/// Returns the value if the <see cref="Result{TValue,TError}"/>.<see cref="IsSuccess"/>
	/// otherwise throw an <see cref="ResultFailedException"/>.
	/// This method should be avoided in favor of pure functional composition style.
	/// The ugly name have been purposely chosen. 
	/// </summary>
	/// <returns></returns>
	/// <exception cref="ResultFailedException"></exception>
	public TValue GetValueFromSuccessfulResultOrThrowAnExceptionIfTheResultWasFailed()
		=> _value is not null
			? _value
			: throw new ResultFailedException();
}