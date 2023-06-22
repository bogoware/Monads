// ReSharper disable SuggestVarOrType_Elsewhere
// ReSharper disable UnusedParameter.Local
namespace Bogoware.Monads.UnitTests.ResultTests;

public class ResultMapTests
{
	private static readonly Result<Value> _success = new(new Value(0));
	private static readonly Result<Value> _failed = new(new LogicError("Something went wrong"));
	
	[Fact]
	public void Success_map_constant()
	{
		Result<string> actual = _success.Map("success");
		actual.IsSuccess.Should().BeTrue();
	}
	
	[Fact]
	public void Failure_map_constant()
	{
		Result<string> actual = _failed.Map("success");
		actual.IsFailure.Should().BeTrue();
	}
	
	[Fact]
	public void Success_map_voidFunction()
	{
		Result<string> actual = _success.Map(() => "success");
		actual.IsSuccess.Should().BeTrue();
	}
	
	[Fact]
	public void Failure_map_voidFunction()
	{
		Result<string> actual = _failed.Map(() => "success");
		actual.IsFailure.Should().BeTrue();
	}
	
	[Fact]
	public async Task Success_map_asyncVoidFunction()
	{
		Result<string> actual = await _success.Map(() => Task.FromResult("success"));
		actual.IsSuccess.Should().BeTrue();
	}
	
	[Fact]
	public async Task Failure_map_asyncVoidFunction()
	{
		Result<string> actual = await _failed.Map(() => Task.FromResult("success"));
		actual.IsFailure.Should().BeTrue();
	}
	
	
	[Fact]
	public void Success_map_function()
	{
		Result<string> actual = _success.Map(success => $"success: {_success}");
		actual.IsSuccess.Should().BeTrue();
	}
	
	[Fact]
	public void Failure_map_function()
	{
		Result<string> actual = _failed.Map(success => $"success: {_success}");
		actual.IsFailure.Should().BeTrue();
	}
	
	[Fact]
	public async Task Success_map_asyncFunction()
	{
		Result<string> actual = await _success.Map(success => Task.FromResult($"success: {_success}"));
		actual.IsSuccess.Should().BeTrue();
	}
	
	[Fact]
	public async Task Failure_map_asyncFunction()
	{
		Result<string> actual = await _failed.Map(success => Task.FromResult($"success: {_success}"));
		actual.IsFailure.Should().BeTrue();
	}
}