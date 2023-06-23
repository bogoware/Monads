namespace Bogoware.Monads.UnitTests.MaybeTests;

public class MaybeBindTests
{
	[Fact]
	public void Some_bindsTo_SomeValue_by_action()
	{
		var value = Maybe.Some(new Value(0));
		var newValue = value.Bind(() => Maybe.Some(new AnotherValue(0)));
		newValue.GetType().Should().Be<Maybe<AnotherValue>>();
		newValue.IsSome.Should().BeTrue();
	}
	
	[Fact]
	public void Some_bindsTo_SomeValue_by_function()
	{
		var value = Maybe.Some(new Value(0));
		var newValue = value.Bind(val => Maybe.Some(new AnotherValue(val.Val + 1)));
		newValue.GetType().Should().Be<Maybe<AnotherValue>>();
		newValue.IsSome.Should().BeTrue();
	}
	
	[Fact]
	public void Some_bindsTo_None_by_action()
	{
		var value = Maybe.Some(new Value(0));
		var newValue = value.Bind(Maybe.None<AnotherValue>);
		newValue.GetType().Should().Be<Maybe<AnotherValue>>();
		newValue.IsNone.Should().BeTrue();
	}
	
	[Fact]
	public void Some_bindsTo_None_by_function()
	{
		var value = Maybe.Some(new Value(0));
		var newValue = value.Bind(_ => Maybe.None<AnotherValue>());
		newValue.GetType().Should().Be<Maybe<AnotherValue>>();
		newValue.IsNone.Should().BeTrue();
	}
	
	[Fact]
	public void None_bindsTo_None_by_action()
	{
		var value = Maybe.None<Value>();
		var newValue = value.Bind(() => Maybe.Some(new AnotherValue(0)));
		newValue.GetType().Should().Be<Maybe<AnotherValue>>();
		newValue.IsNone.Should().BeTrue();
	}
	
	[Fact]
	public void None_bindsTo_None_by_function()
	{
		var value = Maybe.None<Value>();
		var newValue = value.Bind(val => Maybe.Some(new AnotherValue(val.Val + 1)));
		newValue.GetType().Should().Be<Maybe<AnotherValue>>();
		newValue.IsNone.Should().BeTrue();
	}
}