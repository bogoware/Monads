namespace Bogoware.Monads.UnitTests;

public class MaybeOperatorsTests
{
	[Fact]
	public void Implicit_conversion_to_Some()
	{
		Maybe<Value> GetSome() => new Value(0);

		var value = GetSome();

		value.Equals(Some(new Value(0))).Should().BeTrue();
	}
	
	[Fact]
	public void Implicit_conversion_to_None()
	{
		Maybe<Value> GetSome() => null;

		var value = GetSome();

		value.Equals(None<Value>()).Should().BeTrue();
	}
}