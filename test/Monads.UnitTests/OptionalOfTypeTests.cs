namespace Bogoware.Monads.UnitTests;

public class OptionalOfTypeTests
{
	class Base
	{
		
	}

	class Derived : Base
	{
		
	}
	
	[Fact]
	public void SuccessfulDowncast_produces_Some()
	{
		Optional<Base> value1 = Some((Base)new Derived());
		Optional<Derived> value2 = value1.OfType<Derived>();

		value2.HasValue.Should().BeTrue();
	}
	
	[Fact]
	public void ImpossibleDowncast_produces_None()
	{
		Optional<string> value1 = Some("");
		Optional<Derived> value2 = value1.OfType<Derived>();

		value2.IsNone.Should().BeTrue();
	}
}