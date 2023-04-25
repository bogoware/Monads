// ReSharper disable SuggestVarOrType_Elsewhere
namespace Bogoware.Monads.UnitTests;

public class ResultBindTests
{
	private static readonly Result<Value, Error> _success = new(new Value(0));
	private static readonly Result<Value, Error> _failed = new(new LogicError("Something went wrong"));

	[Fact]
	public void Success_bind_constant()
	{
		Result<string, Error> actual = _success.Bind(new Result<string, Error>("success"));
		actual.IsSuccess.Should().BeTrue();
	}

	[Fact]
	public void Failure_bind_constant()
	{
		Result<string, Error> actual = _failed.Bind(new Result<string, Error>("success"));
		actual.IsFailure.Should().BeTrue();
	}

	[Fact]
	public void Success_bind_voidFunction()
	{
		Result<string, Error> actual = _success.Bind(() => new Result<string, Error>("success"));
		actual.IsSuccess.Should().BeTrue();
	}

	[Fact]
	public void Failure_bind_voidFunction()
	{
		Result<string, Error> actual = _failed.Bind(() => new Result<string, Error>("success"));
		actual.IsFailure.Should().BeTrue();
	}

	[Fact]
	public async Task Success_bind_asyncVoidFunction()
	{
		Result<string, Error> actual = await _success.Bind(() => Task.FromResult(new Result<string, Error>("success")));
		actual.IsSuccess.Should().BeTrue();
	}

	[Fact]
	public async Task Failure_bind_asyncVoidFunction()
	{
		Result<string, Error> actual = await _failed.Bind(() => Task.FromResult(new Result<string, Error>("success")));
		actual.IsFailure.Should().BeTrue();
	}

	[Fact]
	public void Success_bind_function()
	{
		Result<string, Error> actual = _success.Bind(success => new Result<string, Error>($"success: {success}"));
		actual.IsSuccess.Should().BeTrue();
	}

	[Fact]
	public void Failure_bind_function()
	{
		Result<string, Error> actual = _failed.Bind(success => new Result<string, Error>($"success: {success}"));
		actual.IsFailure.Should().BeTrue();
	}

	[Fact]
	public async Task Success_bind_asyncFunction()
	{
		Result<string, Error> actual =
			await _success.Bind(success => Task.FromResult(new Result<string, Error>($"success: {success}")));
		actual.IsSuccess.Should().BeTrue();
	}

	[Fact]
	public async Task Failure_bind_asyncFunction()
	{
		Result<string, Error> actual =
			await _failed.Bind(success => Task.FromResult(new Result<string, Error>($"success: {success}")));
		actual.IsFailure.Should().BeTrue();
	}
}