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

	[Fact]
	public void Create_successful_with_struct_result()
	{
		var sut = Result.Success(true);
		sut.IsSuccess.Should().BeTrue();
		sut.IsFailure.Should().BeFalse();
	}

	[Fact]
	public void Create_failed_with_struct_result()
	{
		var sut = Result.Failure<bool>(new LogicError("Something went wrong"));
		sut.IsSuccess.Should().BeFalse();
		sut.IsFailure.Should().BeTrue();
	}

	[Fact]
	public void Create_copy_ctor_successful()
	{
		var result1 = Result.Success("value");
		var result2 = new Result<string>(result1);
		result1.IsSuccess.Should().Be(result2.IsSuccess);
		result1.IsFailure.Should().Be(result2.IsFailure);
		result1.GetValueOrThrow().Should().Be(result2.GetValueOrThrow());
	}

	[Fact]
	public void Create_copy_ctor_failure()
	{
		var result1 = Result.Failure<string>("some error");
		var result2 = new Result<string>(result1);
		result1.IsSuccess.Should().Be(result2.IsSuccess);
		result1.IsFailure.Should().Be(result2.IsFailure);
		result1.GetErrorOrThrow().Should().Be(result2.GetErrorOrThrow());
	}

	[Fact]
	public void Create_via_Result_from_successful_result()
	{
		var result1 = Result.Success("value");
		var result2 = Result.Bind(() => result1);
		result1.IsSuccess.Should().Be(result2.IsSuccess);
		result1.IsFailure.Should().Be(result2.IsFailure);
		result1.GetValueOrThrow().Should().Be(result2.GetValueOrThrow());
	}

	[Fact]
	public async Task Create_via_Result_from_successful_result_async()
	{
		var result1 = Result.Success("value");
		var result2 = await Result.Bind(() => Task.FromResult(result1));
		result1.IsSuccess.Should().Be(result2.IsSuccess);
		result1.IsFailure.Should().Be(result2.IsFailure);
		result1.GetValueOrThrow().Should().Be(result2.GetValueOrThrow());
	}
}