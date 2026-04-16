namespace Bogoware.Monads.UnitTests.ResultTests;

public class ResultToStringTests
{
    [Fact]
    public void Success_toString()
    {
        var value = Result.Success("Hello");
        value.ToString().Should().Be("Success(Hello)");
    }

    [Fact]
    public void Failure_toString()
    {
        var value = Result.Failure<string>("oops");
        value.ToString().Should().Be("Failure<String>(oops)");
    }

    [Fact]
    public void Failure_RuntimeError_toString()
    {
        var value = Result.Failure<string>(new RuntimeError(new InvalidOperationException("something broke")));
        value.ToString().Should().Be("Failure<String>(something broke)");
    }

    [Fact]
    public void Success_Unit_toString()
    {
        var value = Result.Unit;
        value.ToString().Should().Be("Success(Unit)");
    }
}
