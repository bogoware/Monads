// ReSharper disable SuggestVarOrType_Elsewhere
// ReSharper disable UnusedParameter.Local
namespace Bogoware.Monads.UnitTests.ResultTests;

public class ResultMapErrorTests
{
	private static readonly Result<Value> _success = new(new Value(0));
	private static readonly Result<Value> _failed = new(new LogicError("Original error"));
	
	[Fact]
	public void Success_mapError_constant()
	{
		Result<Value> actual = _success.MapError(new LogicError("New error"));
		actual.IsSuccess.Should().BeTrue();
	}
	
	[Fact]
	public void Failure_mapError_constant()
	{
		Result<Value> actual = _failed.MapError(new LogicError("New error"));
		actual.IsFailure.Should().BeTrue();
		actual.GetErrorOrThrow().Message.Should().Be("New error");
	}
	
	[Fact]
	public void Success_mapError_voidFunction()
	{
		Result<Value> actual = _success.MapError(() => new LogicError("New error"));
		actual.IsSuccess.Should().BeTrue();
	}
	
	[Fact]
	public void Failure_mapError_voidFunction()
	{
		Result<Value> actual = _failed.MapError(() => new LogicError("New error"));
		actual.IsFailure.Should().BeTrue();
		actual.GetErrorOrThrow().Message.Should().Be("New error");
	}
	
	[Fact]
	public async Task Success_mapError_asyncVoidFunction()
	{
		Result<Value> actual = await _success.MapError(() => Task.FromResult(new LogicError("New error")));
		actual.IsSuccess.Should().BeTrue();
	}
	
	[Fact]
	public async Task Failure_mapError_asyncVoidFunction()
	{
		Result<Value> actual = await _failed.MapError(() => Task.FromResult(new LogicError("New error")));
		actual.IsFailure.Should().BeTrue();
		actual.GetErrorOrThrow().Message.Should().Be("New error");
	}
	
	
	[Fact]
	public void Success_mapError_function()
	{
		Result<Value> actual = _success.MapError(error => new LogicError($"New error"));
		actual.IsSuccess.Should().BeTrue();
	}
	
	[Fact]
	public void Failure_mapError_function()
	{
		Result<Value> actual = _failed.MapError(error => new LogicError($"New error"));
		actual.IsFailure.Should().BeTrue();
		actual.GetErrorOrThrow().Message.Should().Be("New error");
	}
	
	[Fact]
	public async Task Success_mapError_asyncFunction()
	{
		Result<Value> actual = await _success.MapError(error => Task.FromResult(new LogicError($"New error")));
		actual.IsSuccess.Should().BeTrue();
	}
	
	[Fact]
	public async Task Failure_map_asyncFunction()
	{
		Result<Value> actual = await _failed.MapError(error => Task.FromResult(new LogicError($"New error")));
		actual.IsFailure.Should().BeTrue();
		actual.GetErrorOrThrow().Message.Should().Be("New error");
	}
}