namespace Bogoware.Monads.UnitTests.ResultTests;

public class ResultEqualityTests
{
	[Fact]
	public void Success_are_equals()
	{
		var success = Success<string, LogicError>("Hello");
		var other = Success<string, LogicError>("Hello");

		success.Equals(other).Should().BeTrue();
		success.GetHashCode().Should().Be(other.GetHashCode());
	}
	
	[Fact]
	public void Success_arent_equals()
	{
		var success = Success<string, LogicError>("Hello");
		var other = Success<string, LogicError>("World");

		success.Equals(other).Should().BeFalse();
	}
	
	[Fact]
	public void Failure_are_equals()
	{
		var failure = Failure<string, LogicError>(new("Error"));
		var other = Failure<string, LogicError>(new("Error"));

		failure.Equals(other).Should().BeTrue();
		failure.GetHashCode().Should().Be(other.GetHashCode());
	}
	
	[Fact]
	public void Failure_arent_equals()
	{
		var failure = Failure<string, LogicError>(new("Error"));
		var other = Failure<string, LogicError>(new("Another error"));

		failure.Equals(other).Should().BeFalse();
	}
	
	[Fact]
	public void SuccessFailure_arent_equals()
	{
		var failure = Failure<string, LogicError>(new("Error"));
		var success = Success<string, LogicError>("Another error");

		failure.Equals(success).Should().BeFalse();
	}
}