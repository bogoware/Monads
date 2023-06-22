// ReSharper disable UnusedParameter.Local
namespace Bogoware.Monads.UnitTests.ResultTests;

public class ResultEnsureTests
{
	private static readonly Result<Value> _success = new(new Value(0));
	private static readonly Result<Value> _failed = new(new LogicError("Something went wrong"));

	[Fact]
	public void Success_predicateTrue_constant()
	{
		var actual = _success.Ensure(value => true, new LogicError("Failed"));
		actual.IsSuccess.Should().BeTrue();
	}
	
	[Fact]
	public void Success_predicateFalse_constant()
	{
		var actual = _success.Ensure(value => false, new LogicError("Failed"));
		actual.IsFailure.Should().BeTrue();
	}
	
	[Fact]
	public void Failure_predicateTrue_constant()
	{
		var actual = _failed.Ensure(value => true, new LogicError("Failed"));
		actual.IsFailure.Should().BeTrue();
	}
	
	[Fact]
	public void Failure_predicateFalse_constant()
	{
		var actual = _failed.Ensure(value => false, new LogicError("Failed"));
		actual.IsFailure.Should().BeTrue();
	}
	
	[Fact]
	public async Task Success_asyncPredicateTrue_constant()
	{
		var actual = await _success.Ensure(value => Task.FromResult( true), new LogicError("Failed"));
		actual.IsSuccess.Should().BeTrue();
	}
	
	[Fact]
	public async Task Success_asyncPredicateFalse_constant()
	{
		var actual = await _success.Ensure(value => Task.FromResult(false), new LogicError("Failed"));
		actual.IsFailure.Should().BeTrue();
	}
	
	[Fact]
	public async Task Failure_asyncPredicateTrue_constant()
	{
		var actual = await _failed.Ensure(value => Task.FromResult(true), new LogicError("Failed"));
		actual.IsFailure.Should().BeTrue();
	}
	
	[Fact]
	public async Task Failure_asyncPredicateFalse_constant()
	{
		var actual = await _failed.Ensure(value => Task.FromResult(false), new LogicError("Failed"));
		actual.IsFailure.Should().BeTrue();
	}
	
	[Fact]
	public void Success_predicateTrue_function()
	{
		var actual = _success.Ensure(value => true, () => new LogicError("Failed"));
		actual.IsSuccess.Should().BeTrue();
	}
	
	[Fact]
	public void Success_predicateFalse_function()
	{
		var actual = _success.Ensure(value => false, () => new LogicError("Failed"));
		actual.IsFailure.Should().BeTrue();
	}
	
	[Fact]
	public async Task Success_predicateTrue_asyncFunction()
	{
		var actual = await _success.Ensure(value => true, () => Task.FromResult(new LogicError("Failed")));
		actual.IsSuccess.Should().BeTrue();
	}
	
	[Fact]
	public async Task Success_predicateFalse_asyncFunction()
	{
		var actual = await _success.Ensure(value => false, () => Task.FromResult(new LogicError("Failed")));
		actual.IsFailure.Should().BeTrue();
	}
}