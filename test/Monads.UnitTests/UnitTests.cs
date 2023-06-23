namespace Bogoware.Monads.UnitTests;

public class UnitTests
{
	[Fact]
	public void Singleton()
	{
		var unit1 = Unit.Instance;
		var unit2 = Unit.Instance;
		unit1.Should().BeSameAs(unit2);
	}
	
	[Fact]
	public void Equals_success()
	{
		var unit1 = Unit.Instance;
		var unit2 = Unit.Instance;
		unit1.Equals(unit2).Should().BeTrue();
	}
	
	[Fact]
	public void GetHashcode_test()
	{
		Unit.Instance.GetHashCode().Should().Be(0);
	}
	
	[Fact]
	public void ToString_test()
	{
		Unit.Instance.ToString().Should().Be("Unit");
	}
}