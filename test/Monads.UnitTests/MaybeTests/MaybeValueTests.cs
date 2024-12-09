namespace Bogoware.Monads.UnitTests.MaybeTests;

public class MaybeValueTests
{
    [Fact]
    public void Some_returns_itsValue()
    {
        var some = Maybe.Some(new Value(666));
        var value = some.Value;

        value.Should().Be(new Value(666));
    }
    
    [Fact]
    public void None_returns_null()
    {
        var none = Maybe.None<Value>();
        var value = none.Value;

        value.Should().BeNull();
    }
    
    [Fact]
    public void None_returns_default()
    {
        var none = Maybe.None<Guid>();
        var value = none.Value;

        value.Should().Be(default(Guid));
    }
}