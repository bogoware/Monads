namespace Bogoware.Monads.UnitTests;

public class MaybeBindTests
{
	[Fact]
	public void Some_bindsTo_SomeValue_by_action()
	{
		var value = Some(new Value(0));
		var newValue = value.Bind(() => Some(new AnotherValue(0)));
		newValue.GetType().Should().Be<Maybe<AnotherValue>>();
		newValue.HasValue.Should().BeTrue();
	}
	
	[Fact]
	public void Some_bindsTo_SomeValue_by_function()
	{
		var value = Some(new Value(0));
		var newValue = value.Bind(val => Some(new AnotherValue(val.Val + 1)));
		newValue.GetType().Should().Be<Maybe<AnotherValue>>();
		newValue.HasValue.Should().BeTrue();
	}
	
	[Fact]
	public void Some_bindsTo_None_by_action()
	{
		var value = Some(new Value(0));
		var newValue = value.Bind(None<AnotherValue>);
		newValue.GetType().Should().Be<Maybe<AnotherValue>>();
		newValue.IsNone.Should().BeTrue();
	}
	
	[Fact]
	public void Some_bindsTo_None_by_function()
	{
		var value = Some(new Value(0));
		var newValue = value.Bind(_ => None<AnotherValue>());
		newValue.GetType().Should().Be<Maybe<AnotherValue>>();
		newValue.IsNone.Should().BeTrue();
	}
	
	[Fact]
	public void None_bindsTo_None_by_action()
	{
		var value = None<Value>();
		var newValue = value.Bind(() => Some(new AnotherValue(0)));
		newValue.GetType().Should().Be<Maybe<AnotherValue>>();
		newValue.IsNone.Should().BeTrue();
	}
	
	[Fact]
	public void None_bindsTo_None_by_function()
	{
		var value = None<Value>();
		var newValue = value.Bind(val => Some(new AnotherValue(val.Val + 1)));
		newValue.GetType().Should().Be<Maybe<AnotherValue>>();
		newValue.IsNone.Should().BeTrue();
	}
}