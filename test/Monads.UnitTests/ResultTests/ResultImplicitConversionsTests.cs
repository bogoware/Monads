namespace Bogoware.Monads.UnitTests.ResultTests;

public class ResultImplicitConversionsTests
{
	private Result<string> GetSuccess() => new("success");
	private Result<string> GetFailure() => new LogicError("success");
	
	[Fact]
	public void Implicit_conversion_from_success()
	{
		Result<string> actual = GetSuccess();
		actual.IsSuccess.Should().BeTrue();
	}
	
	[Fact]
	public void Implicit_conversion_from_failure()
	{
		Result<string> actual = GetFailure();
		actual.IsFailure.Should().BeTrue();
	}
}