namespace Bogoware.Monads.UnitTests.Boilerplate;

public interface ICallInspector
{
	void MethodVoid();
	void MethodWithValueArg(Value _);
	void MethodWithMaybeArg(Maybe<Value> _);
	Task MethodVoidAsync();
	Task MethodWithValueArgAsync(Value _);
	Task MethodWithMaybeArgAsync(Maybe<Value> _);
}