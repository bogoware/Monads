namespace Bogoware.Monads.UnitTests.ResultTests;

public class ResultValueAndErrorTests
{
    [Fact]
    public void WhenSuccess_gettingError_throws()
    {
        var sut = Result.Success(1);
        var act = () => sut.Error;
        
        sut.Value.Should().Be(1);
        act.Should().Throw<ResultSuccessException>().WithMessage("*successful*");
    }
    
    [Fact]
    public void WhenFailure_gettingValue_throws()
    {
        var error = new LogicError("Error");
        var sut = Result.Failure<int>(error);
        var act = () => sut.Value;

        sut.Error.Should().Be(error);
        act.Should().Throw<ResultFailedException>().WithMessage("*failed*");
    }
}