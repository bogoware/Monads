namespace Bogoware.Monads.UnitTests.ResultTests;

public class ResultValueTests
{
    [Fact]
    public void Success_returns_itsValue_andNullError()
    {
        var sut = Result.Success(new Value(666));

        sut.Value.Should().NotBeNull();
        sut.Error.Should().BeNull();
    }
    
    [Fact]
    public void Failure_returns_nullValue_andItsError()
    {
        var sut = Result.Failure<Value>("Error");

        sut.Value.Should().BeNull();
        sut.Error.Should().NotBeNull();
    }
    
    [Fact]
    public void Failure_returns_defaultValue_andItsError()
    {
        var sut = Result.Failure<Guid>("Error");
        
        sut.Value.Should().Be(default(Guid));
        sut.Error.Should().NotBeNull();
    }
}