// ReSharper disable UnusedMemberInSuper.Global

namespace Bogoware.Monads;

public interface IResult
{
	bool IsSuccess { get; }
	bool IsFailure { get; }
}

public interface IResult<in TValue> : IResult
{
}