// ReSharper disable UnusedMemberInSuper.Global
// ReSharper disable TypeParameterCanBeVariant

namespace Bogoware.Monads;

public interface IResult
{
	bool IsSuccess { get; }
	bool IsFailure { get; }
	public Error GetErrorOrThrow();
}

public interface IResult<TValue> : IResult
{
	public TValue GetValueOrThrow();
}