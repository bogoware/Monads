namespace Bogoware.Monads.UnitTests.ResultTests;

public class ResultCreationTests
{
	[Fact]
	public void Create_successful_result()
	{
		var sut = Result.Success(new Value(0));
		sut.IsSuccess.Should().BeTrue();
		sut.IsFailure.Should().BeFalse();
	}
	
	[Fact]
	public void Create_failed_result()
	{
		var sut = Result.Failure<Value>(new LogicError("Something went wrong"));
		sut.IsSuccess.Should().BeFalse();
		sut.IsFailure.Should().BeTrue();
	}
	
	[Fact]
	public void Create_successful_unitResult()
	{
		var sut = Result.Unit;
		sut.IsSuccess.Should().BeTrue();
		sut.IsFailure.Should().BeFalse();
	}
	
	[Fact]
	public void Create_failed_unitResult()
	{
		Result<Unit> sut = new LogicError("Something went wrong");
		sut.IsSuccess.Should().BeFalse();
		sut.IsFailure.Should().BeTrue();
	}
	
	[Fact]
	public void GetValue_works_with_successfulResults()
	{
		var sut = Result.Unit;
		var unit = sut.GetValueOrThrow();
		unit.Should().NotBeNull();
	}
	
	[Fact]
	public void GetValue_works_with_failedResults()
	{
		Result<Unit> sut = new LogicError("Something went wrong");

		sut
			.Invoking(_ => _.GetValueOrThrow())
			.Should().ThrowExactly<ResultFailedException>();
	}
	
	[Fact]
	public void GetError_works_with_failedResults()
	{
		Result<Unit> sut = new LogicError("Something went wrong");
		var unit = sut.GetErrorOrThrow();
		unit.Should().NotBeNull();
	}
	
	[Fact]
	public void GetError_works_with_successResults()
	{
		var sut = Result.Unit;

		sut
			.Invoking(_ => _.GetErrorOrThrow())
			.Should().ThrowExactly<ResultSuccessException>();
	}

	
}