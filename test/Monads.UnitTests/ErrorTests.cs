namespace Bogoware.Monads.UnitTests;

public class ErrorTests
{
	[Fact]
	public void CreateRuntimeError()
	{
		var sut = new RuntimeError(new("Message"));
		sut.Exception.Should().NotBeNull();
		sut.Message.Should().Be("Message");
	}

	[Fact]
	public void CreateLogicError()
	{
		var sut = new LogicError("Message");
		sut.Message.Should().Be("Message");
	}

	[Fact]
	public void AggregateError_noMessage()
	{
		var sut = new AggregateError(new LogicError("One"), new LogicError("Two"));
		sut.Message.Should().NotBeNullOrEmpty();
		sut.Errors.Should().HaveCount(2);
	}
	
	[Fact]
	public void AggregateError_enumerable_coverage()
	{
		IEnumerable<Error> errors = new List<Error>() { new LogicError("One"), new LogicError("Two") };
		var sut = new AggregateError(errors);
		sut.Message.Should().NotBeNullOrEmpty();
		sut.Errors.Should().HaveCount(2);
	}
	
	[Fact]
	public void AggregateError_twoErrors_coverage()
	{
		var sut = new AggregateError("Two logic errors", new LogicError("One"), new LogicError("Two"));
		sut.Message.Should().NotBeNullOrEmpty();
		sut.Errors.Should().HaveCount(2);
	}
}