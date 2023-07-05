namespace Bogoware.Monads.UnitTests.ResultTests;

public class RessultThrowIfFailureTests
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
}