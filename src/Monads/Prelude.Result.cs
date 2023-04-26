namespace Bogoware.Monads;

public static partial class Prelude
{
	public static Result<Unit, TError> UnitSuccess<TError>() where TError : Error
		=> new (Monads.Unit.Instance);
	
	public static Result<Unit, TError> UnitFailure<TError>(TError error) where TError : Error
		=> new (error);
	
	public static Result<TValue, TError> Success<TValue, TError>(TValue value) where TError : Error
		=> new (value);
	
	public static Result<TValue, TError> Failure<TValue, TError>(TError error) where TError : Error
		=> new (error);
	
	public static Result<TValue, LogicError> Success<TValue>(TValue value) 
		=> new (value);
	
	public static Result<TValue, LogicError> Failure<TValue>(string message)
		=> new (new LogicError(message));
	
	public static Result<Unit, RuntimeError> Try(Action action)
	{
		RuntimeError? error = null;
		try
		{
			action();
		}
		catch (Exception ex)
		{
			error = new(ex);
		}

		return error is null
			? UnitSuccess<RuntimeError>()
			: UnitFailure(error);
	}

	public static async Task<Result<Unit, RuntimeError>> Try(Func<Task> action)
	{
		RuntimeError? error = null;
		try
		{
			await action();
		}
		catch (Exception ex)
		{
			error = new(ex);
		}

		return error is null
			? UnitSuccess<RuntimeError>()
			: UnitFailure(error);
	}

	public static Result<TValue, RuntimeError> Try<TValue>(Func<TValue> function)
	{
		RuntimeError? error = null;
		TValue? value = default;
		try
		{
			value = function();
		}
		catch (Exception ex)
		{
			error = new(ex);
		}

		return error is null
			? Success<TValue, RuntimeError>(value!)
			: Failure<TValue, RuntimeError>(error);
	}
	
	public static async Task<Result<TValue, RuntimeError>> Try<TValue>(Func<Task<TValue>> function)
	{
		RuntimeError? error = null;
		TValue? value = default;
		try
		{
			value = await function();
		}
		catch (Exception ex)
		{
			error = new(ex);
		}

		return error is null
			? Success<TValue, RuntimeError>(value!)
			: Failure<TValue, RuntimeError>(error);
	}
}