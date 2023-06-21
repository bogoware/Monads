// ReSharper disable UnusedMember.Global
namespace Bogoware.Monads;

/// <summary>
/// Thrown when attempting to instantiate a <see cref="Maybe{T}"/> from an empty value.
/// </summary>
public class MaybeNoneException : ArgumentNullException
{
	private const string ERROR_MESSAGE = "The Maybe is None";
	public MaybeNoneException():base(ERROR_MESSAGE)
	{
	}
	public MaybeNoneException(Exception inner)
		: base(ERROR_MESSAGE, inner)
	{
	}
}