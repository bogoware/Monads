namespace Bogoware.Monads.UnitTests.ResultTests;

public class ResultEqualityTests
{
	[Fact]
	public void Success_are_equals()
	{
		var success = Result.Success("Hello");
		var other = Result.Success("Hello");

		success.Equals(other).Should().BeTrue();
		success.GetHashCode().Should().Be(other.GetHashCode());
	}
	
	[Fact]
	public void Success_arent_equals()
	{
		var success = Result.Success("Hello");
		var other = Result.Success("World");

		success.Equals(other).Should().BeFalse();
	}
	
	[Fact]
	public void Failure_are_equals()
	{
		var failure = Result.Failure<Value>(new LogicError("Error"));
		Result<Value> other = new LogicError("Error");

		failure.Equals(other).Should().BeTrue();
		failure.GetHashCode().Should().Be(other.GetHashCode());
	}
	
	[Fact]
	public void Failure_arent_equals()
	{
		var failure = Result.Failure<Value>(new LogicError("Error"));
		Result<Value> other = new LogicError("Another error");

		failure.Equals(other).Should().BeFalse();
	}
}