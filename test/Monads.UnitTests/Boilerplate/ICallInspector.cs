namespace Bogoware.Monads.UnitTests.Boilerplate;

public interface ICallInspector
{
	void MethodVoid();
	void MethodWithValueArg(Value _);
	void MethodWithOptionalArg(Optional<Value> _);
	Task MethodVoidAsync();
	Task MethodWithValueArgAsync(Value _);
	Task MethodWithOptionalArgAsync(Optional<Value> _);
}