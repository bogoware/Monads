namespace Bogoware.Monads.UnitTests.ResultTests;

public class ResultValueTypeTests
{
    [Fact]
    public void Result_with_value_type_should_be_successful()
    {
        var result = Result.Success(1);
        result.IsSuccess.Should().BeTrue();
        result.GetValueOrThrow().Should().Be(1);
    }
    
    [Fact]
    public void Result_with_value_type_should_be_failed()
    {
        var result = Result.Failure<int>("error");
        result.IsFailure.Should().BeTrue();
    }
}