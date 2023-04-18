// ReSharper disable ArrangeObjectCreationWhenTypeNotEvident
namespace Bogoware.Monads.UnitTests;

public class OptionalDefaultTests
{
	[Fact]
	public void Some_defaults_to_itsValue()
	{
		var some = Some(new Value(666));
		var value = some.Default(new Value(0));

		value.Should().Be(new Value(666));
	}
	
	[Fact]
	public void None_defaults_to_defaultValue()
	{
		var some = None<Value>();
		var value = some.Default(new Value(0));

		value.Should().Be(new Value(0));
	}
	
	[Fact]
	public void Some_lazyDefaults_to_itsValue()
	{
		var some = Some(new Value(666));
		var value = some.Default(() => new Value(0));

		value.Should().Be(new Value(666));
	}
	
	[Fact]
	public void None_lazyDefaults_to_defaultValue()
	{
		var some = None<Value>();
		var value = some.Default(() => new Value(0));

		value.Should().Be(new Value(0));
	}
}