namespace Bogoware.Monads.UnitTests.Boilerplate;

public interface ICallInspector
{
	void MethodVoid();
	void MethodWithValueArg(Value _);
	void MethodWithErrorArg(LogicError _);
	void MethodWithMaybeArg(Maybe<Value> _);
	void MethodWithResultArg(Result<Value, LogicError> _);
	Task MethodVoidAsync();
	Task MethodWithValueArgAsync(Value _);
	Task MethodWithErrorArgAsync(LogicError _);
	Task MethodWithMaybeArgAsync(Maybe<Value> _);
	Task MethodWithResultArgAsync(Result<Value, LogicError> _);
}