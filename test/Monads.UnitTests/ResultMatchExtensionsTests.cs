namespace Bogoware.Monads.UnitTests;

public class ResultMatchExtensionsTests
{
	private static readonly Result<Value, Error> _success = new(new Value(0));

	private static readonly Result<Value, Error> _failed = new(new LogicError("Something went wrong"));

	[Fact]
	public void Success_matches_constant_constant()
	{
		_success.Match("success", "failure").Should().Be("success");
	}

	[Fact]
	public void Failure_matches_constant_constant()
	{
		_failed.Match("success", "failure").Should().Be("failure");
	}

	[Fact]
	public void Success_matches_voidFunction_constant()
	{
		_success.Match(() => "success", "failure").Should().Be("success");
	}

	[Fact]
	public void Failure_matches_constant_voidFunction()
	{
		_failed.Match("success", () => "failure").Should().Be("failure");
	}

	[Fact]
	public void Success_matches_voidFunction_voidFunction()
	{
		_success.Match(() => "success", () => "failure").Should().Be("success");
	}

	[Fact]
	public void Failed_matches_voidFunction_voidFunction()
	{
		_failed.Match(() => "success", () => "failure").Should().Be("failure");
	}
	
	[Fact]
	public async Task Success_matches_asyncVoidFunction_constant()
	{
		(await _success.Match(() => Task.FromResult("success"), "failure")).Should().Be("success");
	}
	
	[Fact]
	public async Task Failed_matches_constant_asyncVoidFunction()
	{
		(await _success.Match("success", () => Task.FromResult("failure"))).Should().Be("success");
	}
	
	[Fact]
	public async Task Success_matches_asyncVoidFunction_asyncVoidFunction()
	{
		(await _success.Match(() => Task.FromResult("success"), () => Task.FromResult("failure"))).Should().Be("success");
	}
	
	[Fact]
	public void Success_matches_successFunction_constant()
	{
		var actual = _success.Match(success => $"success: {success.Val}", "failure");
		actual.Should().StartWith("success");
	}
	
	[Fact]
	public void Failure_matches_successFunction_constant()
	{
		var actual = _failed.Match(success => $"success: {success.Val}", "failure");
		actual.Should().StartWith("failure");
	}
	
	[Fact]
	public void Success_matches_successFunction_voidFunction()
	{
		var actual = _success.Match(success => $"success: {success.Val}", () => "failure");
		actual.Should().StartWith("success");
	}
	
	[Fact]
	public void Failure_matches_successFunction_voidFunction()
	{
		var actual = _failed.Match(success => $"success: {success.Val}", () => "failure");
		actual.Should().StartWith("failure");
	}
	
	[Fact]
	public async Task Success_matches_constant_asyncErrorFunction()
	{
		var actual = await _success.Match("success", error => Task.FromResult($"failure: {error.Message}"));
		actual.Should().StartWith("success");
	}
	
	[Fact]
	public async Task Failure_matches_constant_asyncErrorFunction()
	{
		var actual = await _failed.Match("success", error => Task.FromResult($"failure: {error.Message}"));
		actual.Should().StartWith("failure");
	}
	
	[Fact]
	public async Task Success_matches_voidSuccessFunction_asyncErrorFunction()
	{
		var actual = await _success.Match(() => "success", error => Task.FromResult($"failure: {error.Message}"));
		actual.Should().StartWith("success");
	}
	
	[Fact]
	public async Task Failure_matches_voidSuccessFunction_asyncErrorFunction()
	{
		var actual = await _failed.Match(() => "success", error => Task.FromResult($"failure: {error.Message}"));
		actual.Should().StartWith("failure");
	}
	
	[Fact]
	public async Task Success_matches_asyncSuccessFunction_constant()
	{
		var actual = await _success.Match(success => Task.FromResult($"success: {success.Val}"), "failure");
		actual.Should().StartWith("success");
	}
	
	[Fact]
	public async Task Failure_matches_asyncSuccessFunction_constant()
	{
		var actual = await _failed.Match(success => Task.FromResult($"success: {success.Val}"), "failure");
		actual.Should().StartWith("failure");
	}
	
	[Fact]
	public async Task Success_matches_asyncSuccessFunction_voidErrorFunction()
	{
		var actual = await _success.Match(success => Task.FromResult($"success: {success.Val}"), () => "failure");
		actual.Should().StartWith("success");
	}
	
	[Fact]
	public async Task Failure_matches_asyncSuccessFunction_voidErrorFunction()
	{
		var actual = await _failed.Match(success => Task.FromResult($"success: {success.Val}"), () => "failure");
		actual.Should().StartWith("failure");
	}
}