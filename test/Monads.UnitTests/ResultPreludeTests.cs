namespace Bogoware.Monads.UnitTests;

public class ResultPreludeTests
{
	[Fact]
	public void TryAction_should_return_a_succesfullUnitResultRuntime()
	{
		void Action()
		{
		}

		var result = Try((Action)Action);
		result.IsSuccess.Should().BeTrue();
	}

	[Fact]
	public void TryAction_should_return_a_failedUnitResultRuntime()
	{
		void Action() => throw new();
		var result = Try((Action)Action);
		result.IsFailure.Should().BeTrue();
	}
	
	[Fact]
	public async Task TryActionAsync_should_return_a_successfulUnitResultRuntime()
	{
		Task Action() => Task.FromResult(Unit());
		var result = await Try(Action);
		result.IsSuccess.Should().BeTrue();
	}

	[Fact]
	public async Task TryActionAsync_should_return_a_failedUnitResultRuntime()
	{
		Task Action() => Task.FromException(new());
		var result = await Try(Action);
		result.IsFailure.Should().BeTrue();
	}
	
	[Fact]
	public void TryAction_should_return_a_succesfulResultRuntime()
	{
		Value Action()
		{
			return new(0);
		}

		var result = Try(Action);
		result.IsSuccess.Should().BeTrue();
	}

	[Fact]
	public void TryAction_should_return_a_failedResultRuntime()
	{
		Value Action()
		{
			throw new NotImplementedException();
		}
		var result = Try(Action);
		result.IsFailure.Should().BeTrue();
	}
	
	[Fact]
	public async Task TryActionAsync_should_return_a_succesfulResultRuntime()
	{
		Task<Value> Action() => Task.FromResult(new Value(0));

		var result = await Try(Action);
		result.IsSuccess.Should().BeTrue();
	}

	[Fact]
	public async Task TryActionAsync_should_return_a_failedResultRuntime()
	{
		Task<Value> Action()
		{
			throw new NotImplementedException();
		}
		
		var result = await Try(Action);
		result.IsFailure.Should().BeTrue();
	}
}