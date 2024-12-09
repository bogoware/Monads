namespace Bogoware.Monads;

public class MaybeNoneError(string message) : LogicError(message)
{
    public static readonly MaybeNoneError Default = new("The maybe is none.");
}