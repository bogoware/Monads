namespace Bogoware.Monads.UnitTests.ResultTests;

public class ResultTests
{
	[Fact]
	public void TryAction_should_return_a_successfulUnitResultRuntime()
	{
		void Action()
		{
		}

		var result = Result.Execute(Action);
		result.IsSuccess.Should().BeTrue();
	}

	[Fact]
	public void TryAction_should_return_a_failedUnitResultRuntime()
	{
		void Action() => throw new();
		var result = Result.Execute(Action);
		result.IsFailure.Should().BeTrue();
	}
	
	[Fact]
	public async Task TryActionAsync_should_return_a_successfulUnitResultRuntime()
	{
		Task Action() => Task.FromResult(Result.Unit);
		var result = await Result.Execute(Action);
		result.IsSuccess.Should().BeTrue();
	}

	[Fact]
	public async Task TryActionAsync_should_return_a_failedUnitResultRuntime()
	{
		Task Action() => Task.FromException(new());
		var result = await Result.Execute(Action);
		result.IsFailure.Should().BeTrue();
	}
	
	[Fact]
	public void TryAction_should_return_a_successfulResultRuntime()
	{
		Value Action()
		{
			return new(0);
		}

		var result = Result.Execute(Action);
		result.IsSuccess.Should().BeTrue();
	}

	[Fact]
	public void TryAction_should_return_a_failedResultRuntime()
	{
		Value Action()
		{
			throw new NotImplementedException();
		}
		var result = Result.Execute(Action);
		result.IsFailure.Should().BeTrue();
	}
	
	[Fact]
	public async Task TryActionAsync_should_return_a_successfulResultRuntime()
	{
		Task<Value> Action() => Task.FromResult(new Value(0));

		var result = await Result.Execute(Action);
		result.IsSuccess.Should().BeTrue();
	}

	[Fact]
	public async Task TryActionAsync_should_return_a_failedResultRuntime()
	{
		Task<Value> Action()
		{
			throw new NotImplementedException();
		}
		
		var result = await Result.Execute(Action);
		result.IsFailure.Should().BeTrue();
	}
}