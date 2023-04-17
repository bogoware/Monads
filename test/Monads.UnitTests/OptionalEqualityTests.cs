namespace Bogoware.Monads.UnitTests;

public class OptionalEqualityTests
{
	[Fact]
	public void Some_are_notEquals_to_null()
	{
		var value1 = Some(new Value(0));
		value1.Equals(null).Should().BeFalse();
	}
	
	[Fact]
	public void Some_are_equals()
	{
		var value1 = Some(new Value(0));
		var value2 = Some(new Value(0));
		value1.Should().Be(value2);
		value1.GetHashCode().Should().Be(value2.GetHashCode());
	}
	
	[Fact]
	public void Some_are_not_equals_by_inner_value()
	{
		var value1 = Some(new Value(0));
		var value2 = Some(new Value(1));
		value1.Should().NotBe(value2);
	}
	
	[Fact]
	public void Some_are_not_equals_by_inner_type()
	{
		var value1 = Some(new Value(0));
		var value2 = Some(new AnotherValue(0));
		value1.Should().NotBe(value2);
	}
	
	[Fact]
	public void None_are_equals_by_type()
	{
		var value1 = None<Value>();
		var value2 = None<Value>();
		value1.Should().Be(value2);
		value1.GetHashCode().Should().Be(value2.GetHashCode());
	}
	
	[Fact]
	public void None_are_not_equals_by_type()
	{
		var value1 = None<Value>();
		var value2 = None<AnotherValue>();
		value1.Should().NotBe(value2);
	}
	
	[Fact]
	public void Some_equals_match_equality_op()
	{
		var value1 = Some(new Value(0));
		var value2 = Some(new Value(0));
		Assert.True(value1 == value2);
	}
	
	[Fact]
	public void Some_notEquals_dontMatch_equality_op()
	{
		var value1 = Some(new Value(0));
		var value2 = Some(new Value(1));
		Assert.True(value1 != value2);
	}
}