namespace Bogoware.Monads.UnitTests;

public class MaybePreludeTests
{
	[Fact]
	public void Unit_success()
	{
		var value = Unit();
		value.Equals(Unit.Instance).Should().Be(true);
	} 
	
	[Fact]
	public void None_success()
	{
		var value = None();
		value.Equals(None<Unit>()).Should().Be(true);
	}
}