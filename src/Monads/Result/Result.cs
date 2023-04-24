namespace Bogoware.Monads;

public readonly struct Result<TValue, TError>: IResult
	where TError : Error
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
	/// </summary>
	/// <returns></returns>
	/// <exception cref="ResultFailedException"></exception>
	public TValue GetValueOrThrow()
		=> _value is not null
			? _value
			: throw new ResultFailedException(_error!);
	
	public TResult Match<TResult>(Func<TValue, TResult> successful, Func<TError, TResult> failure)
		=> _value is not null ? successful(_value) : failure(_error!);
	
	public Task<TResult> Match<TResult>(Func<TValue, Task<TResult>> successful, Func<TError, TResult> failure)
		=> _value is not null ? successful(_value) : Task.FromResult(failure(_error!));
	
	public Task<TResult> Match<TResult>(Func<TValue, TResult> successful, Func<TError, Task<TResult>> failure)
		=> _value is not null ? Task.FromResult(successful(_value)) : failure(_error!);
	
	public  Task<TResult> Match<TResult>(Func<TValue, Task<TResult>> successful, Func<TError, Task<TResult>> failure)
		=> _value is not null ? successful(_value) : failure(_error!);

	public Result<TNewValue, TError> Map<TNewValue>(TNewValue newValue)
		=> _value is null ? new(_error!) : new(newValue);
	
	public Result<TNewValue, TError> Map<TNewValue>(Func<TNewValue> functor)
		=> _value is null ? new(_error!) : new(functor());
	
	public async Task<Result<TNewValue, TError>> Map<TNewValue>(Func<Task<TNewValue>> functor)
		=> _value is null ? new(_error!) : new(await functor());
	
	public Result<TNewValue, TError> Map<TNewValue>(Func<TValue, TNewValue> functor)
		=> _value is null ? new(_error!) : new(functor(_value));

	public async Task<Result<TNewValue, TError>> Map<TNewValue>(Func<TValue, Task<TNewValue>> functor)
		=> _value is null ? new(_error!) : new(await functor(_value));


	// TODO: Map, Bind, Recover, Ensure, IfSuccess, IfFailure, Execute
	// TODO: Error Equals…
}