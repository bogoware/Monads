namespace Bogoware.Monads.UnitTests.MaybeTests;

public class MaybeEqualityTests
{
	[Fact]
	public void ObjectEquals_null_return_false()
	{
		var value1 = Maybe.Some(new Value(0));
		object value2 = null!;
		value1.Equals(value2).Should().BeFalse();
	}
	
	[Fact]
	public void ObjectEquals_return_true()
	{
		var value1 = Maybe.Some(new Value(0));
		object value2 = Maybe.Some(new Value(0));
		value1.Equals(value2).Should().BeTrue();
	}
	
	[Fact]
	public void Some_are_notEquals_to_null()
	{
		var value1 = Maybe.Some(new Value(0));
		value1.Equals(null).Should().BeFalse();
	}
	
	[Fact]
	public void Some_are_equals()
	{
		var value1 = Maybe.Some(new Value(0));
		var value2 = Maybe.Some(new Value(0));
		value1.Equals(value2).Should().BeTrue();
		value1.GetHashCode().Should().Be(value2.GetHashCode());
	}
	
	[Fact]
	public void Some_are_not_equals_by_inner_value()
	{
		var value1 = Maybe.Some(new Value(0));
		var value2 = Maybe.Some(new Value(1));
		value1.Equals(value2).Should().BeFalse();
	}
	
	[Fact]
	public void Some_are_not_equals_by_inner_type()
	{
		var value1 = Maybe.Some(new Value(0));
		var value2 = (object)Maybe.Some(new AnotherValue(0));
		value1.Equals(value2).Should().BeFalse();
	}
	
	[Fact]
	public void None_are_equals_by_type()
	{
		var value1 = Maybe.None<Value>();
		var value2 = Maybe.None<Value>();
		value1.Equals(value2).Should().BeTrue();
		value1.GetHashCode().Should().Be(value2.GetHashCode());
	}
	
	[Fact]
	public void None_are_not_equals_by_type()
	{
		var value1 = Maybe.None<Value>();
		var value2 = (object)Maybe.None<AnotherValue>();
		value1.Equals(value2).Should().BeFalse();
	}
	
	[Fact]
	public void Some_equals_match_equality_op()
	{
		var value1 = Maybe.Some(new Value(0));
		var value2 = Maybe.Some(new Value(0));
		Assert.True(value1 == value2);
	}
	
	[Fact]
	public void Some_notEquals_dontMatch_equality_op()
	{
		var value1 = Maybe.Some(new Value(0));
		var value2 = Maybe.Some(new Value(1));
		Assert.True(value1 != value2);
	}
}