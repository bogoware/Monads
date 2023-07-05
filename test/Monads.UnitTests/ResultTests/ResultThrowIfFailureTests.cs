namespace Bogoware.Monads.UnitTests.ResultTests;

public class ResultThrowIfFailureTests
{
	[Fact]
	public void ResultThrowIfFailure_WhenResultIsFailure_ThrowsResultFailedException()
	{
		// Arrange
		var result = Result.Failure<string>("error");

		// Act
		var act = () => result.ThrowIfFailure();

		// Assert
		act.Should().Throw<ResultFailedException>();
	}
	
	[Fact]
	public void ResultThrowIfFailure_WhenResultIsSuccess_ReturnsResult()
	{
		// Arrange
		var result = Result.Success("value");

		// Act
		var act = () => result.ThrowIfFailure();

		// Assert
		act.Should().NotThrow<ResultFailedException>();
	}
	
	[Fact]
	public async Task ResultThrowIfFailureAsync_WhenResultIsFailure_ThrowsResultFailedException()
	{
		// Arrange
		var result = Task.FromResult(Result.Failure<string>("error"));

		// Act
		var act = () => result.ThrowIfFailure();

		// Assert
		await act.Should().ThrowAsync<ResultFailedException>();
	}
	
	[Fact]
	public async Task ResultThrowIfFailureAsync_WhenResultIsSuccess_ReturnsResult()
	{
		// Arrange
		var result = Task.FromResult(Result.Success("value"));

		// Act
		var act = () => result.ThrowIfFailure();

		// Assert
		await act.Should().NotThrowAsync<ResultFailedException>();
	}
	
	[Fact]
	public async Task ResultThrowIfFailureAsync_WhenResultIsFailure_ThrowsResultFailedException_useCase()
	{
		// Arrange
		var result = Task.FromResult(Result.Failure<Guid>("error"));

		try
		{
			await result
				.ExecuteIfFailure(() => { })
				.ThrowIfFailure();
		}
		catch (ResultFailedException)
		{
			
		}
	}
}