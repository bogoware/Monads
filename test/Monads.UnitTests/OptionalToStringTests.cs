namespace Bogoware.Monads.UnitTests;

public class OptionalToStringTests
{
	[Fact]
	public void Some_toString()
	{
		var value = Some("Hello");
		value.ToString().Should().Be("Some(Hello)");
	}
	[Fact]
	public void None_toString()
	{
		var value = None<string>();
		value.ToString().Should().Be("None<String>()");
	}
	[Fact]
	public void NoneUnit_toString()
	{
		var value = None<Unit>();
		value.ToString().Should().Be("None<Unit>()");
	}
	[Fact]
	public void NoneGenerics_toString()
	{
		var value = None<List<string>>();
		value.ToString().Should().Be("None<List<String>>()");
	}
}