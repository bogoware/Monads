using Moq;

namespace Bogoware.Monads.UnitTests.ResultTests;

public class ResultExecuteTests
{
	private static readonly Result<Value> _success = new(new Value(0));
	private static readonly Result<Value> _failed = new(new LogicError("Something went wrong"));
	private readonly Mock<ICallInspector> _inspector = new();

	[Fact]
	public void Successful_executeIfSuccess_calls_voidFunction()
	{
		_success.ExecuteIfSuccess(_inspector.Object.MethodVoid);
		_inspector.Verify(i => i.MethodVoid());
	}

	[Fact]
	public void Successful_executeIfFailure_doesntCall_voidFunction()
	{
		_success.ExecuteIfFailure(_inspector.Object.MethodVoid);
		_inspector.VerifyNoOtherCalls();
	}

	[Fact]
	public void Failure_executeIfSuccess_doesntCall_voidFunction()
	{
		_failed.ExecuteIfSuccess(_inspector.Object.MethodVoid);
		_inspector.VerifyNoOtherCalls();
	}

	[Fact]
	public void Failure_executeIfFailure_calls_voidFunction()
	{
		_failed.ExecuteIfFailure(_inspector.Object.MethodVoid);
		_inspector.Verify(i => i.MethodVoid());
	}

	[Fact]
	public async Task Successful_executeIfSuccess_calls_asyncVoidFunction()
	{
		await _success.ExecuteIfSuccess(_inspector.Object.MethodVoidAsync);
		_inspector.Verify(i => i.MethodVoidAsync());
	}

	[Fact]
	public async Task Successful_executeIfFailure_doesntCall_asyncVoidFunction()
	{
		await _success.ExecuteIfFailure(_inspector.Object.MethodVoidAsync);
		_inspector.VerifyNoOtherCalls();
	}

	[Fact]
	public async Task Failure_executeIfSuccess_doesntCall_asyncVoidFunction()
	{
		await _failed.ExecuteIfSuccess(_inspector.Object.MethodVoidAsync);
		_inspector.VerifyNoOtherCalls();
	}

	[Fact]
	public async Task Failure_executeIfFailure_calls_asyncVoidFunction()
	{
		await _failed.ExecuteIfFailure(_inspector.Object.MethodVoidAsync);
		_inspector.Verify(i => i.MethodVoidAsync());
	}

	[Fact]
	public void Successful_executeIfSuccess_calls_function()
	{
		_success.ExecuteIfSuccess(_inspector.Object.MethodWithValueArg);
		_inspector.Verify(i => i.MethodWithValueArg(It.IsAny<Value>()));
	}

	[Fact]
	public void Successful_executeIfFailure_doesntCall_function()
	{
		_success.ExecuteIfFailure(_inspector.Object.MethodWithErrorArg);
		_inspector.VerifyNoOtherCalls();
	}

	[Fact]
	public void Failure_executeIfSuccess_doesntCall_function()
	{
		_failed.ExecuteIfSuccess(_inspector.Object.MethodWithValueArg);
		_inspector.VerifyNoOtherCalls();
	}

	[Fact]
	public void Failure_executeIfFailure_calls_function()
	{
		_failed.ExecuteIfFailure(_inspector.Object.MethodWithErrorArg);
		_inspector.Verify(i => i.MethodWithErrorArg(It.IsAny<LogicError>()));
	}

	[Fact]
	public async Task Successful_executeIfSuccess_calls_asyncFunction()
	{
		await _success.ExecuteIfSuccess(_inspector.Object.MethodWithValueArgAsync);
		_inspector.Verify(i => i.MethodWithValueArgAsync(It.IsAny<Value>()));
	}

	[Fact]
	public async Task Successful_executeIfFailure_doesntCall_asyncFunction()
	{
		await _success.ExecuteIfFailure(_inspector.Object.MethodWithErrorArgAsync);
		_inspector.VerifyNoOtherCalls();
	}

	[Fact]
	public async Task Failure_executeIfSuccess_doesntCall_asyncFunction()
	{
		await _failed.ExecuteIfSuccess(_inspector.Object.MethodWithValueArgAsync);
		_inspector.VerifyNoOtherCalls();
	}

	[Fact]
	public async Task Failure_executeIfFailure_calls_asyncFunction()
	{
		await _failed.ExecuteIfFailure(_inspector.Object.MethodWithErrorArgAsync);
		_inspector.Verify(i => i.MethodWithErrorArgAsync(It.IsAny<LogicError>()));
	}

	[Fact]
	public void Successful_execute_calls_voidFunction()
	{
		_success.Execute(_inspector.Object.MethodVoid);
		_inspector.Verify(i => i.MethodVoid());
	}

	[Fact]
	public void Failure_execute_calls_voidFunction()
	{
		_failed.Execute(_inspector.Object.MethodVoid);
		_inspector.Verify(i => i.MethodVoid());
	}

	[Fact]
	public async Task Successful_execute_calls_asyncVoidFunction()
	{
		await _success.Execute(_inspector.Object.MethodVoidAsync);
		_inspector.Verify(i => i.MethodVoidAsync());
	}

	[Fact]
	public async Task Failure_execute_calls_asyncVoidFunction()
	{
		await _failed.Execute(_inspector.Object.MethodVoidAsync);
		_inspector.Verify(i => i.MethodVoidAsync());
	}

	[Fact]
	public void Successful_execute_calls_function()
	{
		_success.Execute(_inspector.Object.MethodWithResultArg);
		_inspector.Verify(i => i.MethodWithResultArg(It.IsAny<Result<Value>>()));
	}

	[Fact]
	public void Failure_execute_calls_function()
	{
		_failed.Execute(_inspector.Object.MethodWithResultArg);
		_inspector.Verify(i => i.MethodWithResultArg(It.IsAny<Result<Value>>()));
	}
	
	[Fact]
	public async Task Successful_execute_calls_asyncFunction()
	{
		await _success.Execute(_inspector.Object.MethodWithResultArgAsync);
		_inspector.Verify(i => i.MethodWithResultArgAsync(It.IsAny<Result<Value>>()));
	}

	[Fact]
	public async Task Failure_execute_calls_asyncFunction()
	{
		await _failed.Execute(_inspector.Object.MethodWithResultArgAsync);
		_inspector.Verify(i => i.MethodWithResultArgAsync(It.IsAny<Result<Value>>()));
	}
}