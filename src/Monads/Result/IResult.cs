// ReSharper disable UnusedMemberInSuper.Global
// ReSharper disable UnusedTypeParameter

namespace Bogoware.Monads;

public interface IResult
{
	bool IsSuccess { get; }
	bool IsFailure { get; }
	public Error GetErrorOrThrow();
}

public interface IResult<in TValue> : IResult
{
	//public TValue GetValueOrThrow();
}