namespace Bogoware.Monads.UnitTests.MaybeTests;

public class MaybeOperatorsTests
{
	[Fact]
	public void Implicit_conversion_to_Some()
	{
		Maybe<Value> GetSome() => new Value(0);

		var value = GetSome();

		value.Equals(Maybe.Some(new Value(0))).Should().BeTrue();
	}
	
	[Fact]
	public void Implicit_conversion_to_None()
	{
		Maybe<Value> GetSome() => null;

		var value = GetSome();

		value.Equals(Maybe.None<Value>()).Should().BeTrue();
	}
}