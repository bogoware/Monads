// ReSharper disable SuggestVarOrType_Elsewhere
namespace Bogoware.Monads.UnitTests.ResultTests;

public class ResultBindTests
{
	private static readonly Result<Value> _success = new(new Value(0));
	private static readonly Result<Value> _failed = new(new LogicError("Something went wrong"));

	[Fact]
	public void Success_bind_constant()
	{
		Result<string> actual = _success.Bind(new Result<string>("success"));
		actual.IsSuccess.Should().BeTrue();
	}

	[Fact]
	public void Failure_bind_constant()
	{
		Result<string> actual = _failed.Bind(new Result<string>("success"));
		actual.IsFailure.Should().BeTrue();
	}

	[Fact]
	public void Success_bind_voidFunction()
	{
		Result<string> actual = _success.Bind(() => new Result<string>("success"));
		actual.IsSuccess.Should().BeTrue();
	}

	[Fact]
	public void Failure_bind_voidFunction()
	{
		Result<string> actual = _failed.Bind(() => new Result<string>("success"));
		actual.IsFailure.Should().BeTrue();
	}

	[Fact]
	public async Task Success_bind_asyncVoidFunction()
	{
		Result<string> actual = await _success.Bind(() => Task.FromResult(new Result<string>("success")));
		actual.IsSuccess.Should().BeTrue();
	}

	[Fact]
	public async Task Failure_bind_asyncVoidFunction()
	{
		Result<string> actual = await _failed.Bind(() => Task.FromResult(new Result<string>("success")));
		actual.IsFailure.Should().BeTrue();
	}

	[Fact]
	public void Success_bind_function()
	{
		Result<string> actual = _success.Bind(success => new Result<string>($"success: {success}"));
		actual.IsSuccess.Should().BeTrue();
	}

	[Fact]
	public void Failure_bind_function()
	{
		Result<string> actual = _failed.Bind(success => new Result<string>($"success: {success}"));
		actual.IsFailure.Should().BeTrue();
	}

	[Fact]
	public async Task Success_bind_asyncFunction()
	{
		Result<string> actual =
			await _success.Bind(success => Task.FromResult(new Result<string>($"success: {success}")));
		actual.IsSuccess.Should().BeTrue();
	}

	[Fact]
	public async Task Failure_bind_asyncFunction()
	{
		Result<string> actual =
			await _failed.Bind(success => Task.FromResult(new Result<string>($"success: {success}")));
		actual.IsFailure.Should().BeTrue();
	}
}