namespace Bogoware.Monads.UnitTests.Boilerplate;

public interface ICallInspector
{
	void MethodVoid();
	void MethodWithValueArg(Value _);
	void MethodWithErrorArg(Error _);
	void MethodWithMaybeArg(Maybe<Value> _);
	void MethodWithResultArg(Result<Value> _);
	Task MethodVoidAsync();
	Task MethodWithValueArgAsync(Value _);
	Task MethodWithErrorArgAsync(Error _);
	Task MethodWithMaybeArgAsync(Maybe<Value> _);
	Task MethodWithResultArgAsync(Result<Value> _);
}