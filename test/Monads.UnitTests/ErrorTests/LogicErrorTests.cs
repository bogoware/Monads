namespace Bogoware.Monads.UnitTests.ErrorTests;

public class LogicErrorTests
{
	[Fact]
	public void Equality_pattern_success()
	{
		var error1 = new LogicError("error");
		var error2 = new LogicError("error");

		error1.Equals((object)error2).Should().BeTrue();
		error1.GetHashCode().Should().Be(error2.GetHashCode());
		(error1 == error2).Should().BeTrue();
		(error1 != error2).Should().BeFalse();
	}

	[Fact]
	public void Error_ToString()
	{
		var error = new LogicError("Hello World");
		error.ToString().Should().Be("""LogicError: "Hello World".""");
	}
}