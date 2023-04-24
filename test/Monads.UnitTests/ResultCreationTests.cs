namespace Bogoware.Monads.UnitTests;

public class ResultCreationTests
{
	[Fact]
	public void Create_successful_result()
	{
		var sut = Success<Value, LogicError>(new(0));
		sut.IsSuccess.Should().BeTrue();
		sut.IsFailure.Should().BeFalse();
	}
	
	[Fact]
	public void Create_failed_result()
	{
		var sut = Failure<Value, LogicError>(new("Something went wrong"));
		sut.IsSuccess.Should().BeFalse();
		sut.IsFailure.Should().BeTrue();
	}
	
	[Fact]
	public void Create_successful_unitResult()
	{
		var sut = UnitSuccess<LogicError>();
		sut.IsSuccess.Should().BeTrue();
		sut.IsFailure.Should().BeFalse();
	}
	
	[Fact]
	public void Create_failed_unitResult()
	{
		var sut = UnitFailure<LogicError>(new("Something went wrong"));
		sut.IsSuccess.Should().BeFalse();
		sut.IsFailure.Should().BeTrue();
	}
	
	[Fact]
	public void GetValue_works_with_successfulResults()
	{
		var sut = UnitSuccess<LogicError>();
		var unit = sut.GetValueOrThrow();
		unit.Should().NotBeNull();
	}
	
	[Fact]
	public void GetValue_works_with_failedResults()
	{
		var sut = UnitFailure<LogicError>(new("Something went wrong"));

		sut
			.Invoking(_ => _.GetValueOrThrow())
			.Should().ThrowExactly<ResultFailedException>();
	}
}