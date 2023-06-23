namespace Bogoware.Monads.UnitTests.MaybeTests;

public class MaybeToStringTests
{
	[Fact]
	public void Some_toString()
	{
		var value = Maybe.Some("Hello");
		value.ToString().Should().Be("Some(Hello)");
	}
	[Fact]
	public void None_toString()
	{
		var value = Maybe.None<string>();
		value.ToString().Should().Be("None<String>()");
	}
	[Fact]
	public void NoneUnit_toString()
	{
		var value = Maybe.None<Unit>();
		value.ToString().Should().Be("None<Unit>()");
	}
	[Fact]
	public void NoneGenerics_toString()
	{
		var value = Maybe.None<List<string>>();
		value.ToString().Should().Be("None<List<String>>()");
	}
}