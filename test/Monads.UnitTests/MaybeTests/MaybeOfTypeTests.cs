// ReSharper disable SuggestVarOrType_Elsewhere
namespace Bogoware.Monads.UnitTests.MaybeTests;

public class MaybeOfTypeTests
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
		Maybe<Base> value1 = Maybe.Some((Base)new Derived());
		Maybe<Derived> value2 = value1.OfType<Derived>();

		value2.IsSome.Should().BeTrue();
	}
	
	[Fact]
	public void ImpossibleDowncast_produces_None()
	{
		Maybe<string> value1 = Maybe.Some("");
		Maybe<Derived> value2 = value1.OfType<Derived>();

		value2.IsNone.Should().BeTrue();
	}
}