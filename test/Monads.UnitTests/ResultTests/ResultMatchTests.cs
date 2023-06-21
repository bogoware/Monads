namespace Bogoware.Monads.UnitTests.ResultTests;

public class ResultMatchTests
{
	private static readonly Result<Value, Error> _success = new(new Value(0));
	private static readonly Result<Value, Error> _failed = new(new LogicError("Something went wrong"));

	[Fact]
	public void Success_matches_valueFunction_errorFunction()
	{
		var actual = _success.Match(success => $"success: {success.Val}", error => $"failure: {error.Message}");
		actual.Should().StartWith("success:");
	}
	
	[Fact]
	public void Failed_matches_valueFunction_errorFunction()
	{
		var actual = _failed.Match(success => $"success: {success.Val}", error => $"failure: {error.Message}");
		actual.Should().StartWith("failure:");
	}
	
	[Fact]
	public async Task Success_matches_asyncValueFunction_errorFunction()
	{
		var actual = await _success.Match(success => Task.FromResult($"success: {success.Val}"), error => $"failure: {error.Message}");
		actual.Should().StartWith("success:");
	}
	
	[Fact]
	public async Task Failure_matches_asyncValueFunction_errorFunction()
	{
		var actual = await _failed.Match(success => Task.FromResult($"success: {success.Val}"), error => $"failure: {error.Message}");
		actual.Should().StartWith("failure:");
	}
	
	[Fact]
	public async Task Success_matches_valueFunction_asyncErrorFunction()
	{
		var actual = await _success.Match(success => $"success: {success.Val}", error => Task.FromResult( $"failure: {error.Message}"));
		actual.Should().StartWith("success:");
	}
	
	[Fact]
	public async Task Failure_matches_valueFunction_asyncErrorFunction()
	{
		var actual = await _failed.Match(success => $"success: {success.Val}", error => Task.FromResult( $"failure: {error.Message}"));
		actual.Should().StartWith("failure:");
	}
	
	[Fact]
	public async Task Success_matches_asyncValueFunction_asyncErrorFunction()
	{
		var actual = await _success.Match(success => Task.FromResult($"success: {success.Val}"), error => Task.FromResult( $"failure: {error.Message}"));
		actual.Should().StartWith("success:");
	}
	
	[Fact]
	public async Task Failure_matches_asyncValueFunction_asyncErrorFunction()
	{
		var actual = await _failed.Match(success => Task.FromResult($"success: {success.Val}"), error => Task.FromResult( $"failure: {error.Message}"));
		actual.Should().StartWith("failure:");
	}
}