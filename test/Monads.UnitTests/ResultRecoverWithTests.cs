using Xunit.Sdk;

namespace Bogoware.Monads.UnitTests;

public class ResultRecoverWithTests
{
	private static readonly Result<Value, Error> _success = new(new Value(0));
	private static readonly Result<Value, Error> _failed = new(new LogicError("Something went wrong"));
	
	[Fact]
	public void Success_recover_constant()
	{
		Result<Value, Error> actual = _success.RecoverWith(new Value(1));
		actual.GetValueOrThrow().Val.Should().Be(0);
	}
	
	[Fact]
	public void Failure_recover_constant()
	{
		Result<Value, Error> actual = _failed.RecoverWith(new Value(1));
		actual.GetValueOrThrow().Val.Should().Be(1);
	}
	
	[Fact]
	public void Success_recover_voidFunction()
	{
		Result<Value, Error> actual = _success.RecoverWith(() => new Value(1));
		actual.GetValueOrThrow().Val.Should().Be(0);
	}
	
	[Fact]
	public void Failure_recover_voidFunction()
	{
		Result<Value, Error> actual = _failed.RecoverWith(() => new Value(1));
		actual.GetValueOrThrow().Val.Should().Be(1);
	}
	
	[Fact]
	public async Task Success_recover_asyncVoidFunction()
	{
		Result<Value, Error> actual = await _success.RecoverWith(() => Task.FromResult(new Value(1)));
		actual.GetValueOrThrow().Val.Should().Be(0);
	}
	
	[Fact]
	public async Task Failure_recover_asyncVoidFunction()
	{
		Result<Value, Error> actual = await _failed.RecoverWith(() => Task.FromResult(new Value(1)));
		actual.GetValueOrThrow().Val.Should().Be(1);
	}
	
	[Fact]
	public void Success_recover_function()
	{
		Result<Value, Error> actual = _success.RecoverWith(error => new(1));
		actual.GetValueOrThrow().Val.Should().Be(0);
	}
	
	[Fact]
	public void Failure_recover_function()
	{
		Result<Value, Error> actual = _failed.RecoverWith(error => new(1));
		actual.GetValueOrThrow().Val.Should().Be(1);
	}
	
	[Fact]
	public async Task Success_recover_asyncFunction()
	{
		Result<Value, Error> actual = await _success.RecoverWith(error => Task.FromResult<Value>(new(1)));
		actual.GetValueOrThrow().Val.Should().Be(0);
	}
	
	[Fact]
	public async Task Failure_recover_asyncFunction()
	{
		Result<Value, Error> actual = await _failed.RecoverWith(error => Task.FromResult<Value>(new(1)));
		actual.GetValueOrThrow().Val.Should().Be(1);
	}
}