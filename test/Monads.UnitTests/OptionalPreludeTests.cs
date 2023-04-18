namespace Bogoware.Monads.UnitTests;

public class OptionalPreludeTests
{
	[Fact]
	public void Unit_success()
	{
		var value = Prelude.Unit();
		value.Equals(Unit.Instance).Should().Be(true);
	} 
	
	[Fact]
	public void None_success()
	{
		var value = Prelude.None();
		value.Equals(None<Unit>()).Should().Be(true);
	}
}