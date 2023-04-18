namespace Bogoware.Monads.UnitTests;

public class OptionalOperatorsTests
{
	[Fact]
	public void Implicit_conversion_to_Some()
	{
		Optional<Value> GetSome() => new Value(0);

		var value = GetSome();

		value.Equals(Some(new Value(0))).Should().BeTrue();
	}
	
	[Fact]
	public void Implicit_conversion_to_None()
	{
		Optional<Value> GetSome() => null;

		var value = GetSome();

		value.Equals(None<Value>()).Should().BeTrue();
	}
}