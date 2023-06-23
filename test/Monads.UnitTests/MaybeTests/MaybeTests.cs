namespace Bogoware.Monads.UnitTests.MaybeTests;

public class MaybeTests
{
	[Fact]
	public void Unit_success()
	{
		var value = Result.Unit;
		value.Equals(Unit.Instance).Should().Be(true);
	} 
	
	[Fact]
	public void None_success()
	{
		var value = Maybe.None();
		value.Equals(Maybe.None<Unit>()).Should().Be(true);
	}
}