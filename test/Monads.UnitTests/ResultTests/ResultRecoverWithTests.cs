// ReSharper disable UnusedParameter.Local
// ReSharper disable SuggestVarOrType_Elsewhere
namespace Bogoware.Monads.UnitTests.ResultTests;

public class ResultRecoverWithTests
{
	private static readonly Result<Value> _success = new(new Value(0));
	private static readonly Result<Value> _failed = new(new LogicError("Something went wrong"));
	
	[Fact]
	public void Success_recover_constant()
	{
		Result<Value> actual = _success.RecoverWith(new Value(1));
		actual.GetValueOrThrow().Val.Should().Be(0);
	}
	
	[Fact]
	public void Failure_recover_constant()
	{
		Result<Value> actual = _failed.RecoverWith(new Value(1));
		actual.GetValueOrThrow().Val.Should().Be(1);
	}
	
	[Fact]
	public void Success_recover_voidFunction()
	{
		Result<Value> actual = _success.RecoverWith(() => new(1));
		actual.GetValueOrThrow().Val.Should().Be(0);
	}
	
	[Fact]
	public void Failure_recover_voidFunction()
	{
		Result<Value> actual = _failed.RecoverWith(() => new(1));
		actual.GetValueOrThrow().Val.Should().Be(1);
	}
	
	[Fact]
	public async Task Success_recover_asyncVoidFunction()
	{
		Result<Value> actual = await _success.RecoverWith(() => Task.FromResult(new Value(1)));
		actual.GetValueOrThrow().Val.Should().Be(0);
	}
	
	[Fact]
	public async Task Failure_recover_asyncVoidFunction()
	{
		Result<Value> actual = await _failed.RecoverWith(() => Task.FromResult(new Value(1)));
		actual.GetValueOrThrow().Val.Should().Be(1);
	}
	
	[Fact]
	public void Success_recover_function()
	{
		Result<Value> actual = _success.RecoverWith(error => new(1));
		actual.GetValueOrThrow().Val.Should().Be(0);
	}
	
	[Fact]
	public void Failure_recover_function()
	{
		Result<Value> actual = _failed.RecoverWith(error => new(1));
		actual.GetValueOrThrow().Val.Should().Be(1);
	}
	
	[Fact]
	public async Task Success_recover_asyncFunction()
	{
		Result<Value> actual = await _success.RecoverWith(error => Task.FromResult<Value>(new(1)));
		actual.GetValueOrThrow().Val.Should().Be(0);
	}
	
	[Fact]
	public async Task Failure_recover_asyncFunction()
	{
		Result<Value> actual = await _failed.RecoverWith(error => Task.FromResult<Value>(new(1)));
		actual.GetValueOrThrow().Val.Should().Be(1);
	}
}